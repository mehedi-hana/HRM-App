import { CommonModule } from '@angular/common';
import { Component, computed, OnInit, signal } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CommonService } from '../../services/common.service';
import { EmployeeService } from '../../services/employee.service';
import { DropdownItem } from '../../models/dropdown-item.model';
import { EmployeeDetailDto, EmployeeDocumentDto, EmployeeEducationInfoDto, EmployeeFamilyInfoDto, EmployeeProfessionalCertificationDto } from '../../models/employee-detail.model';
import { EmployeeListDto } from '../../models/employee-list.model';
import { ApiResponse } from '../../models/api-response.model';

@Component({
    selector: 'app-employee-page',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './employee-page.component.html',
    styleUrls: ['./employee-page.component.css'],
})
export class EmployeePageComponent implements OnInit {
    employeeForm: FormGroup;
    employees = signal<EmployeeListDto[]>([]);
    selectedEmployee = signal<EmployeeDetailDto | null>(null);
    selectedEmployeeId = signal<number | null>(null);
    mode = signal<'view' | 'create' | 'edit'>('view');
    errorMessage = signal<string | null>(null);
    previewImage = signal('');
    message = signal<string | null>(null);
    messageType = signal<'success' | 'danger' | null>(null);

    departments = signal<DropdownItem[]>([]);
    designations = signal<DropdownItem[]>([]);
    genders = signal<DropdownItem[]>([]);
    jobTypes = signal<DropdownItem[]>([]);
    employeeTypes = signal<DropdownItem[]>([]);
    maritalStatuses = signal<DropdownItem[]>([]);
    religions = signal<DropdownItem[]>([]);
    sections = signal<DropdownItem[]>([]);
    weekOffs = signal<DropdownItem[]>([]);
    relationships = signal<DropdownItem[]>([]);
    educationLevels = signal<DropdownItem[]>([]);
    educationExaminations = signal<DropdownItem[]>([]);
    educationResults = signal<DropdownItem[]>([]);

    isFormEnabled = computed(() => this.mode() !== 'view');

    constructor(
        private readonly fb: FormBuilder,
        private readonly employeeService: EmployeeService,
        private readonly commonService: CommonService
    ) {
        this.employeeForm = this.buildForm();
    }

    ngOnInit(): void {
        this.loadEmployeeList();
        this.loadDropdowns();
        this.resetFormView();
    }

    private buildForm(): FormGroup {
        return this.fb.group({
            idClient: [10001001],
            id: [null],
            employeeName: ['', [Validators.required, Validators.maxLength(250)]],
            employeeNameBangla: ['', Validators.maxLength(250)],
            employeeImage: [''],
            fatherName: ['', Validators.maxLength(250)],
            motherName: ['', Validators.maxLength(250)],
            idReportingManager: [null],
            idJobType: [null],
            idEmployeeType: [null],
            birthDate: [null],
            joiningDate: [null],
            idGender: [null],
            idReligion: [null],
            idDepartment: [null, Validators.required],
            idSection: [null, Validators.required],
            idDesignation: [null],
            hasOvertime: [false],
            hasAttendenceBonus: [false],
            idWeekOff: [null],
            address: ['', Validators.maxLength(250)],
            presentAddress: ['', Validators.maxLength(250)],
            nationalIdentificationNumber: ['', Validators.maxLength(30)],
            contactNo: ['', Validators.maxLength(250)],
            idMaritalStatus: [null],
            isActive: [true],
            employeeFamilyInfos: this.fb.array([]),
            employeeEducationInfos: this.fb.array([]),
            employeeDocuments: this.fb.array([]),
            employeeProfessionalCertifications: this.fb.array([]),
        });
    }

    get familyInfos(): FormArray {
        return this.employeeForm.get('employeeFamilyInfos') as FormArray;
    }

    get educationInfos(): FormArray {
        return this.employeeForm.get('employeeEducationInfos') as FormArray;
    }

    get documentInfos(): FormArray {
        return this.employeeForm.get('employeeDocuments') as FormArray;
    }

    get certificationInfos(): FormArray {
        return this.employeeForm.get('employeeProfessionalCertifications') as FormArray;
    }

    control(name: string): FormControl {
        return this.employeeForm.get(name) as FormControl;
    }

    addFamilyInfo(): void {
        this.familyInfos.push(this.createFamilyInfoGroup());
    }

    removeFamilyInfo(index: number): void {
        this.familyInfos.removeAt(index);
    }

    addEducationInfo(): void {
        this.educationInfos.push(this.createEducationInfoGroup());
    }

    removeEducationInfo(index: number): void {
        this.educationInfos.removeAt(index);
    }

    addDocumentInfo(): void {
        this.documentInfos.push(this.createDocumentGroup());
    }

    removeDocumentInfo(index: number): void {
        this.documentInfos.removeAt(index);
    }

    addCertificationInfo(): void {
        this.certificationInfos.push(this.createProfessionalCertificationGroup());
    }

    removeCertificationInfo(index: number): void {
        this.certificationInfos.removeAt(index);
    }

    startAdd(): void {
        this.clearError();
        this.mode.set('create');
        this.selectedEmployee.set(null);
        this.selectedEmployeeId.set(null);
        this.resetForm();
        this.previewImage.set('');
        this.setFormEnabled(true);
    }

    enterEdit(): void {
        if (!this.selectedEmployeeId()) {
            return;
        }
        this.clearError();
        this.mode.set('edit');
        this.setFormEnabled(true);
    }

    cancel(): void {
        if (this.mode() === 'create') {
            this.resetFormView();
            return;
        }

        if (this.mode() === 'edit') {
            const id = this.selectedEmployeeId();
            if (id !== null) {
                this.loadEmployeeDetail(id);
                return;
            }
        }

        this.resetFormView();
    }

    save(): void {
        this.clearError();
        if (this.employeeForm.invalid) {
            this.employeeForm.markAllAsTouched();
            return;
        }
        this.employeeService
            .create(this.buildPayload(false))
            .subscribe({
                next: () => {
                    this.loadEmployeeList();
                    this.resetFormView();
                    this.showMessage('Employee saved successfully');
                },
                error: (error) => this.setError(error),
            });
    }

    update(): void {
        this.clearError();
        if (this.employeeForm.invalid) {
            this.employeeForm.markAllAsTouched();
            return;
        }

        const id = this.employeeForm.get('id')?.value;
        if (!id) {
            this.setError('Cannot update employee without an identifier.');
            return;
        }

        this.employeeService
            .update(id, this.buildPayload(true))
            .subscribe({
                next: () => {
                    this.loadEmployeeList();
                    this.loadEmployeeDetail(id);
                    this.showMessage('Employee updated successfully');
                },
                error: (error) => this.setError(error),
            });
    }

    deleteEmployee(): void {
        this.clearError();
        const id = this.selectedEmployeeId();
        if (!id) {
            return;
        }

        if (!confirm('Are you sure?')) {
            return;
        }

        this.employeeService
            .deleteEmployee(id)
            .subscribe({
                next: () => {
                    this.loadEmployeeList();
                    this.resetFormView();
                    this.showMessage('Employee deleted successfully', 'danger');
                },
                error: (error) => this.setError(error),
            });
    }

    selectEmployee(employee: EmployeeListDto): void {
        this.clearError();
        this.loadEmployeeDetail(employee.id);
    }

    onEmployeeImageSelected(event: Event): void {
        const input = event.target as HTMLInputElement;
        const file = input.files?.[0];
        if (!file) {
            return;
        }

        this.readFileAsBase64(file).then((base64) => {
            this.employeeForm.patchValue({ employeeImage: base64 });
            this.previewImage.set(`data:${file.type};base64,${base64}`);
        });
    }

    onDocumentFileSelected(index: number, event: Event): void {
        const input = event.target as HTMLInputElement;
        const file = input.files?.[0];
        if (!file) {
            return;
        }

        this.readFileAsBase64(file).then((base64) => {
            const group = this.documentInfos.at(index) as FormGroup;
            group.patchValue({
                fileName: file.name,
                uploadedFileExtention: file.name.split('.').pop() ?? '',
                uploadedFile: base64,
                uploadDate: new Date().toISOString().slice(0, 10),
            });
        });
    }

    private loadDropdowns(): void {
        this.commonService.getDepartments()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.departments.set(res.data));

        this.commonService.getDesignations()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.designations.set(res.data));

        this.commonService.getGenders()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.genders.set(res.data));

        this.commonService.getJobTypes()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.jobTypes.set(res.data));

        this.commonService.getEmployeeTypes()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.employeeTypes.set(res.data));

        this.commonService.getMaritalStatuses()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.maritalStatuses.set(res.data));

        this.commonService.getReligions()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.religions.set(res.data));

        this.commonService.getSections()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.sections.set(res.data));

        this.commonService.getWeekOffs()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.weekOffs.set(res.data));

        this.commonService.getRelationships()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.relationships.set(res.data));

        this.commonService.getEducationLevels()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.educationLevels.set(res.data));

        this.commonService.getEducationExaminations()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.educationExaminations.set(res.data));

        this.commonService.getEducationResults()
            .pipe(catchError(() => of({ data: [] } as ApiResponse)))
            .subscribe(res => this.educationResults.set(res.data));
    }

    private loadEmployeeList(): void {
        this.employeeService
            .getAll()
            .subscribe({
                next: (res) => this.employees.set(res.data ?? []),
                error: (error) => this.setError(error),
            });
    }

    private loadEmployeeDetail(id: number): void {
        this.employeeService
            .getById(id)
            .subscribe({
                next: (res) => {
                    this.selectedEmployee.set(res.data);
                    this.selectedEmployeeId.set(id);
                    this.patchForm(res.data);
                    this.mode.set('view');
                    this.setFormEnabled(false);
                },
                error: (error) => this.setError(error),
            });
    }

    private patchForm(detail: EmployeeDetailDto): void {
        this.employeeForm.patchValue({
            ...detail,
            birthDate: this.formatDate(detail.birthDate),
            joiningDate: this.formatDate(detail.joiningDate),
            isActive: detail.isActive ?? true,
        });
        this.setFormArray('employeeFamilyInfos', detail.employeeFamilyInfos ?? [], this.createFamilyInfoGroup.bind(this));
        this.setFormArray('employeeEducationInfos', detail.employeeEducationInfos ?? [], this.createEducationInfoGroup.bind(this));
        this.setFormArray('employeeDocuments', detail.employeeDocuments ?? [], this.createDocumentGroup.bind(this));
        this.setFormArray('employeeProfessionalCertifications', detail.employeeProfessionalCertifications ?? [], this.createProfessionalCertificationGroup.bind(this));
        this.previewImage.set(this.toPreviewImage(detail.employeeImage));
    }

    private resetFormView(): void {
        this.resetForm();
        this.mode.set('view');
        this.setFormEnabled(false);
        this.selectedEmployee.set(null);
        this.selectedEmployeeId.set(null);
        this.previewImage.set('');
        this.clearError();
    }

    private resetForm(): void {
        this.employeeForm.reset({
            idClient: 10001001,
            id: null,
            employeeName: '',
            employeeNameBangla: '',
            employeeImage: '',
            fatherName: '',
            motherName: '',
            idReportingManager: null,
            idJobType: null,
            idEmployeeType: null,
            birthDate: null,
            joiningDate: null,
            idGender: null,
            idReligion: null,
            idDepartment: null,
            idSection: null,
            idDesignation: null,
            hasOvertime: false,
            hasAttendenceBonus: false,
            idWeekOff: null,
            address: '',
            presentAddress: '',
            nationalIdentificationNumber: '',
            contactNo: '',
            idMaritalStatus: null,
            isActive: true,
        });

        this.setFormArray('employeeFamilyInfos', [], this.createFamilyInfoGroup.bind(this));
        this.setFormArray('employeeEducationInfos', [], this.createEducationInfoGroup.bind(this));
        this.setFormArray('employeeDocuments', [], this.createDocumentGroup.bind(this));
        this.setFormArray('employeeProfessionalCertifications', [], this.createProfessionalCertificationGroup.bind(this));
    }

    private setFormEnabled(enabled: boolean): void {
        if (enabled) {
            this.employeeForm.enable();
            return;
        }
        this.employeeForm.disable();
    }


    private setFormArray(name: string, values: any[], factory: (value?: any) => FormGroup): void {
        const array = this.employeeForm.get(name) as FormArray;
        array.clear();

        for (const item of values ?? []) {
            array.push(factory(item));
        }
    }

    private createFamilyInfoGroup(value?: EmployeeFamilyInfoDto): FormGroup {
        return this.fb.group({
            id: [value?.id ?? null],
            idGender: [value?.idGender ?? null, Validators.required],
            idRelationship: [value?.idRelationship ?? null, Validators.required],
            name: [value?.name ?? '', [Validators.required, Validators.maxLength(50)]],
            dateOfBirth: [this.formatDate(value?.dateOfBirth)],
            contactNo: [value?.contactNo ?? '', Validators.maxLength(50)],
            currentAddress: [value?.currentAddress ?? '', Validators.maxLength(500)],
            permanentAddress: [value?.permanentAddress ?? '', Validators.maxLength(500)],
        });
    }

    private createEducationInfoGroup(value?: EmployeeEducationInfoDto): FormGroup {
        return this.fb.group({
            id: [value?.id ?? null],
            idEducationLevel: [value?.idEducationLevel ?? null, Validators.required],
            idEducationExamination: [value?.idEducationExamination ?? null, Validators.required],
            idEducationResult: [value?.idEducationResult ?? null, Validators.required],
            major: [value?.major ?? '', [Validators.required, Validators.maxLength(50)]],
            passingYear: [value?.passingYear ?? null, [Validators.required, Validators.min(1900)]],
            instituteName: [value?.instituteName ?? '', [Validators.required, Validators.maxLength(250)]],
            cgpa: [value?.cgpa ?? null],
            examScale: [value?.examScale ?? null],
            marks: [value?.marks ?? null],
            isForeignInstitute: [value?.isForeignInstitute ?? false],
            duration: [value?.duration ?? null],
            achievement: [value?.achievement ?? ''],
        });
    }

    private createDocumentGroup(value?: EmployeeDocumentDto): FormGroup {
        return this.fb.group({
            id: [value?.id ?? null],
            documentName: [value?.documentName ?? '', [Validators.required, Validators.maxLength(200)]],
            fileName: [value?.fileName ?? '', [Validators.required, Validators.maxLength(100)]],
            uploadDate: [this.formatDate(value?.uploadDate) ?? new Date().toISOString().slice(0, 10), Validators.required],
            uploadedFileExtention: [value?.uploadedFileExtention ?? ''],
            uploadedFile: [value?.uploadedFile ?? ''],
        });
    }

    private createProfessionalCertificationGroup(value?: EmployeeProfessionalCertificationDto): FormGroup {
        return this.fb.group({
            id: [value?.id ?? null],
            certificationTitle: [value?.certificationTitle ?? '', [Validators.required, Validators.maxLength(255)]],
            certificationInstitute: [value?.certificationInstitute ?? '', [Validators.required, Validators.maxLength(250)]],
            instituteLocation: [value?.instituteLocation ?? '', [Validators.required, Validators.maxLength(250)]],
            fromDate: [this.formatDate(value?.fromDate), Validators.required],
            toDate: [this.formatDate(value?.toDate)],
        });
    }

    private buildPayload(includeId: boolean): EmployeeDetailDto {
        const raw = this.employeeForm.getRawValue();
        const payload: any = {
            idClient: raw.idClient,
            employeeName: raw.employeeName,
            employeeNameBangla: raw.employeeNameBangla,
            employeeImage: raw.employeeImage || null,
            fatherName: raw.fatherName,
            motherName: raw.motherName,
            idReportingManager: raw.idReportingManager,
            idJobType: raw.idJobType,
            idEmployeeType: raw.idEmployeeType,
            birthDate: raw.birthDate || null,
            joiningDate: raw.joiningDate || null,
            idGender: raw.idGender,
            idReligion: raw.idReligion,
            idDepartment: raw.idDepartment,
            idSection: raw.idSection,
            idDesignation: raw.idDesignation,
            hasOvertime: !!raw.hasOvertime,
            hasAttendenceBonus: !!raw.hasAttendenceBonus,
            idWeekOff: raw.idWeekOff,
            address: raw.address,
            presentAddress: raw.presentAddress,
            nationalIdentificationNumber: raw.nationalIdentificationNumber,
            contactNo: raw.contactNo,
            idMaritalStatus: raw.idMaritalStatus,
            isActive: !!raw.isActive,
            employeeFamilyInfos: raw.employeeFamilyInfos.filter((item: any) => item.name || item.idGender || item.idRelationship),
            employeeEducationInfos: raw.employeeEducationInfos.filter((item: any) => item.major || item.instituteName || item.idEducationLevel),
            employeeDocuments: raw.employeeDocuments.filter((item: any) => item.documentName || item.uploadedFile),
            employeeProfessionalCertifications: raw.employeeProfessionalCertifications.filter((item: any) => item.certificationTitle || item.certificationInstitute),
        };

        if (includeId) {
            payload.id = raw.id;
        }

        return payload;
    }

    private readFileAsBase64(file: File): Promise<string> {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onload = () => {
                const result = reader.result as string;
                const base64 = result.split(',')[1] ?? '';
                resolve(base64);
            };
            reader.onerror = () => reject(reader.error);
            reader.readAsDataURL(file);
        });
    }

    private formatDate(value?: string): string | null {
        if (!value) {
            return null;
        }
        const parsed = new Date(value);
        return Number.isNaN(parsed.getTime()) ? null : parsed.toLocaleDateString('en-CA', { timeZone: 'Asia/Dhaka' }).slice(0, 10);
    }

    private toPreviewImage(value?: string | null): string {
        if (!value) {
            return '';
        }
        return value.startsWith('data:') ? value : `data:image/png;base64,${value}`;
    }

    private setError(error: any): void {
        this.errorMessage.set(error.error?.message ?? 'An unexpected error occurred.');
    }

    private clearError(): void {
        this.errorMessage.set(null);
    }

    private showMessage(msg: string, type: 'success' | 'danger' = 'success') {
        this.message.set(msg);
        this.messageType.set(type);

        setTimeout(() => {
            this.message.set(null);
            this.messageType.set(null);
        }, 3000);
    }
}
