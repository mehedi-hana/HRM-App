import { CommonModule } from '@angular/common';
import { Component, computed, OnInit, signal } from '@angular/core';
import {
  applyEach,
  FieldState,
  form,
  FormField,
  FormRoot,
  maxLength,
  min,
  required,
  SchemaPathTree,
} from '@angular/forms/signals';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ApiResponse } from '../../models/api-response.model';
import { DropdownItem } from '../../models/dropdown-item.model';
import { EmployeeDetailDto } from '../../models/employee-detail.model';
import { EmployeeListDto } from '../../models/employee-list.model';
import { CommonService } from '../../services/common.service';
import { EmployeeService } from '../../services/employee.service';

// ─── Plain-object interfaces for Signal Forms model ───────────────────────────

interface FamilyInfoModel {
  id: number | null;
  idGender: number | null;
  idRelationship: number | null;
  name: string;
  dateOfBirth: string | null;
  contactNo: string;
  currentAddress: string;
  permanentAddress: string;
}

interface EducationInfoModel {
  id: number | null;
  idEducationLevel: number | null;
  idEducationExamination: number | null;
  idEducationResult: number | null;
  major: string;
  passingYear: number | null;
  instituteName: string;
  cgpa: number | null;
  examScale: number | null;
  marks: number | null;
  isForeignInstitute: boolean;
  duration: number | null;
  achievement: string;
}

interface DocumentModel {
  id: number | null;
  documentName: string;
  fileName: string;
  uploadDate: string;
  uploadedFileExtention: string;
  uploadedFile: string;
}

interface CertificationModel {
  id: number | null;
  certificationTitle: string;
  certificationInstitute: string;
  instituteLocation: string;
  fromDate: string | null;
  toDate: string | null;
}

interface EmployeeFormModel {
  idClient: number;
  id: number | null;
  employeeName: string;
  employeeNameBangla: string;
  employeeImage: string;
  fatherName: string;
  motherName: string;
  idReportingManager: number | null;
  idJobType: number | null;
  idEmployeeType: number | null;
  birthDate: string | null;
  joiningDate: string | null;
  idGender: number | null;
  idReligion: number | null;
  idDepartment: number | null;
  idSection: number | null;
  idDesignation: number | null;
  hasOvertime: boolean;
  hasAttendenceBonus: boolean;
  idWeekOff: number | null;
  address: string;
  presentAddress: string;
  nationalIdentificationNumber: string;
  contactNo: string;
  idMaritalStatus: number | null;
  isActive: boolean;
  employeeFamilyInfos: FamilyInfoModel[];
  employeeEducationInfos: EducationInfoModel[];
  employeeDocuments: DocumentModel[];
  employeeProfessionalCertifications: CertificationModel[];
}

// ─── Default / blank values ───────────────────────────────────────────────────

function blankFamilyInfo(): FamilyInfoModel {
  return {
    id: null,
    idGender: null,
    idRelationship: null,
    name: '',
    dateOfBirth: null,
    contactNo: '',
    currentAddress: '',
    permanentAddress: '',
  };
}

function blankEducationInfo(): EducationInfoModel {
  return {
    id: null,
    idEducationLevel: null,
    idEducationExamination: null,
    idEducationResult: null,
    major: '',
    passingYear: null,
    instituteName: '',
    cgpa: null,
    examScale: null,
    marks: null,
    isForeignInstitute: false,
    duration: null,
    achievement: '',
  };
}

function blankDocument(): DocumentModel {
  return {
    id: null,
    documentName: '',
    fileName: '',
    uploadDate: new Date().toISOString().slice(0, 10),
    uploadedFileExtention: '',
    uploadedFile: '',
  };
}

function blankCertification(): CertificationModel {
  return {
    id: null,
    certificationTitle: '',
    certificationInstitute: '',
    instituteLocation: '',
    fromDate: null,
    toDate: null,
  };
}

function blankEmployeeModel(): EmployeeFormModel {
  return {
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
    employeeFamilyInfos: [],
    employeeEducationInfos: [],
    employeeDocuments: [],
    employeeProfessionalCertifications: [],
  };
}

// ─── Sub-schemas ──────────────────────────────────────────────────────────────

function familyInfoSchema(item: SchemaPathTree<FamilyInfoModel>): void {
  required(item.idGender, { message: 'Gender is required' });
  required(item.idRelationship, { message: 'Relationship is required' });
  required(item.name, { message: 'Name is required' });
  maxLength(item.name, 50, { message: 'Name must not exceed 50 characters' });
  maxLength(item.contactNo, 50, { message: 'Contact No must not exceed 50 characters' });
  maxLength(item.currentAddress, 500, { message: 'Current address must not exceed 500 characters' });
  maxLength(item.permanentAddress, 500, { message: 'Permanent address must not exceed 500 characters' });
}

function educationInfoSchema(item: SchemaPathTree<EducationInfoModel>): void {
  required(item.idEducationLevel, { message: 'Education level is required' });
  required(item.idEducationExamination, { message: 'Examination is required' });
  required(item.idEducationResult, { message: 'Result is required' });
  required(item.major, { message: 'Major is required' });
  maxLength(item.major, 50, { message: 'Major must not exceed 50 characters' });
  required(item.passingYear, { message: 'Passing year is required' });
  min(item.passingYear, 1900, { message: 'Passing year must be 1900 or later' });
  required(item.instituteName, { message: 'Institute name is required' });
  maxLength(item.instituteName, 250, { message: 'Institute name must not exceed 250 characters' });
}

function documentSchema(item: SchemaPathTree<DocumentModel>): void {
  required(item.documentName, { message: 'Document name is required' });
  maxLength(item.documentName, 200, { message: 'Document name must not exceed 200 characters' });
  required(item.fileName, { message: 'File name is required' });
  maxLength(item.fileName, 100, { message: 'File name must not exceed 100 characters' });
  required(item.uploadDate, { message: 'Upload date is required' });
}

function certificationSchema(item: SchemaPathTree<CertificationModel>): void {
  required(item.certificationTitle, { message: 'Certification title is required' });
  maxLength(item.certificationTitle, 255, { message: 'Title must not exceed 255 characters' });
  required(item.certificationInstitute, { message: 'Institute is required' });
  maxLength(item.certificationInstitute, 250, { message: 'Institute must not exceed 250 characters' });
  required(item.instituteLocation, { message: 'Location is required' });
  maxLength(item.instituteLocation, 250, { message: 'Location must not exceed 250 characters' });
  required(item.fromDate, { message: 'From date is required' });
}

// ─── Component ────────────────────────────────────────────────────────────────

@Component({
  selector: 'app-employee-page',
  standalone: true,
  imports: [CommonModule, FormField, FormRoot],
  templateUrl: './employee-page.component.new.html',
})
export class EmployeePageNewComponent implements OnInit {

  // ── UI state ───────────────────────────────────────────────────────────────
  employees = signal<EmployeeListDto[]>([]);
  selectedEmployee = signal<EmployeeDetailDto | null>(null);
  selectedEmployeeId = signal<number | null>(null);
  mode = signal<'view' | 'create' | 'edit'>('view');
  errorMessage = signal<string | null>(null);
  previewImage = signal('');
  message = signal<string | null>(null);
  messageType = signal<'success' | 'warning' | 'danger' | null>(null);

  // ── Dropdown data ──────────────────────────────────────────────────────────
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
  reportingManagers = signal<DropdownItem[]>([]);

  // ── Signal Form model & form tree ─────────────────────────────────────────
  employeeModel = signal<EmployeeFormModel>(blankEmployeeModel());

  employeeForm = form(this.employeeModel, (s) => {
    required(s.employeeName, { message: 'Employee name is required' });
    maxLength(s.employeeName, 250, { message: 'Employee name must not exceed 250 characters' });
    maxLength(s.employeeNameBangla, 250, { message: 'Employee name (Bangla) must not exceed 250 characters' });
    required(s.idDepartment, { message: 'Department is required' });
    required(s.idSection, { message: 'Section is required' });
    maxLength(s.fatherName, 250, { message: 'Father name must not exceed 250 characters' });
    maxLength(s.motherName, 250, { message: 'Mother name must not exceed 250 characters' });
    maxLength(s.address, 250, { message: 'Address must not exceed 250 characters' });
    maxLength(s.presentAddress, 250, { message: 'Present address must not exceed 250 characters' });
    maxLength(s.nationalIdentificationNumber, 30, { message: 'NID must not exceed 30 characters' });
    maxLength(s.contactNo, 250, { message: 'Contact No must not exceed 250 characters' });

    applyEach(s.employeeFamilyInfos, familyInfoSchema);
    applyEach(s.employeeEducationInfos, educationInfoSchema);
    applyEach(s.employeeDocuments, documentSchema);
    applyEach(s.employeeProfessionalCertifications, certificationSchema);
  });

  // ── Derived ────────────────────────────────────────────────────────────────
  isFormEnabled = computed(() => this.mode() !== 'view');

  constructor(
    private readonly employeeService: EmployeeService,
    private readonly commonService: CommonService,
  ) {}

  ngOnInit(): void {
    this.loadEmployeeList();
    this.loadDropdowns();
    this.resetFormView();
  }

  // ─── Array helpers ─────────────────────────────────────────────────────────

  /** Read current array from the model, push a blank item, write back. */
  addFamilyInfo(): void {
    this.employeeModel.update(m => ({
      ...m,
      employeeFamilyInfos: [...m.employeeFamilyInfos, blankFamilyInfo()],
    }));
  }

  removeFamilyInfo(index: number): void {
    this.employeeModel.update(m => ({
      ...m,
      employeeFamilyInfos: m.employeeFamilyInfos.filter((_, i) => i !== index),
    }));
  }

  addEducationInfo(): void {
    this.employeeModel.update(m => ({
      ...m,
      employeeEducationInfos: [...m.employeeEducationInfos, blankEducationInfo()],
    }));
  }

  removeEducationInfo(index: number): void {
    this.employeeModel.update(m => ({
      ...m,
      employeeEducationInfos: m.employeeEducationInfos.filter((_, i) => i !== index),
    }));
  }

  addDocumentInfo(): void {
    this.employeeModel.update(m => ({
      ...m,
      employeeDocuments: [...m.employeeDocuments, blankDocument()],
    }));
  }

  removeDocumentInfo(index: number): void {
    this.employeeModel.update(m => ({
      ...m,
      employeeDocuments: m.employeeDocuments.filter((_, i) => i !== index),
    }));
  }

  addCertificationInfo(): void {
    this.employeeModel.update(m => ({
      ...m,
      employeeProfessionalCertifications: [
        ...m.employeeProfessionalCertifications,
        blankCertification(),
      ],
    }));
  }

  removeCertificationInfo(index: number): void {
    this.employeeModel.update(m => ({
      ...m,
      employeeProfessionalCertifications: m.employeeProfessionalCertifications.filter(
        (_, i) => i !== index,
      ),
    }));
  }

  // ─── Mode transitions ──────────────────────────────────────────────────────

  startAdd(): void {
    this.clearError();
    this.mode.set('create');
    this.selectedEmployee.set(null);
    this.selectedEmployeeId.set(null);
    this.employeeModel.set(blankEmployeeModel());
    this.previewImage.set('');
  }

  enterEdit(): void {
    if (!this.selectedEmployeeId()) return;
    this.clearError();
    this.mode.set('edit');
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

  // ─── CRUD actions ──────────────────────────────────────────────────────────

  save(): void {
    this.clearError();
    if (this.employeeForm().invalid()) {
      // Touch all fields so errors become visible in the template
      this.touchAll();
      return;
    }
    this.employeeService.create(this.buildPayload(false)).subscribe({
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
    if (this.employeeForm().invalid()) {
      this.touchAll();
      return;
    }
    const id = this.employeeModel().id;
    if (!id) {
      this.setError('Cannot update employee without an identifier.');
      return;
    }
    this.employeeService.update(id, this.buildPayload(true)).subscribe({
      next: () => {
        this.loadEmployeeList();
        this.loadEmployeeDetail(id);
        this.showMessage('Employee updated successfully', 'warning');
      },
      error: (error) => this.setError(error),
    });
  }

  deleteEmployee(): void {
    this.clearError();
    const id = this.selectedEmployeeId();
    if (!id) return;
    if (!confirm('Are you sure?')) return;
    this.employeeService.deleteEmployee(id).subscribe({
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

  // ─── File handling ─────────────────────────────────────────────────────────

  onEmployeeImageSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];
    if (!file) return;
    this.readFileAsBase64(file).then((base64) => {
      this.employeeForm.employeeImage().value.set(base64);
      this.previewImage.set(`data:${file.type};base64,${base64}`);
    });
  }

  onDocumentFileSelected(index: number, event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];
    if (!file) return;
    this.readFileAsBase64(file).then((base64) => {
      // Update document fields at the given index via field-level set()
      const docItem = this.employeeForm.employeeDocuments[index];
      docItem.fileName().value.set(file.name);
      docItem.uploadedFileExtention().value.set(file.name.split('.').pop() ?? '');
      docItem.uploadedFile().value.set(base64);
      docItem.uploadDate().value.set(new Date().toISOString().slice(0, 10));
    });
  }

  // ─── Private helpers ───────────────────────────────────────────────────────

  private loadDropdowns(): void {
    const load = <T>(obs: ReturnType<typeof this.commonService.getDepartments>, target: ReturnType<typeof signal<T[]>>) =>
      obs.pipe(catchError(() => of({ data: [] } as ApiResponse))).subscribe(res => (target as any).set(res.data));

    load(this.commonService.getDepartments(), this.departments);
    load(this.commonService.getDesignations(), this.designations);
    load(this.commonService.getGenders(), this.genders);
    load(this.commonService.getJobTypes(), this.jobTypes);
    load(this.commonService.getEmployeeTypes(), this.employeeTypes);
    load(this.commonService.getMaritalStatuses(), this.maritalStatuses);
    load(this.commonService.getReligions(), this.religions);
    load(this.commonService.getSections(), this.sections);
    load(this.commonService.getWeekOffs(), this.weekOffs);
    load(this.commonService.getRelationships(), this.relationships);
    load(this.commonService.getEducationLevels(), this.educationLevels);
    load(this.commonService.getEducationExaminations(), this.educationExaminations);
    load(this.commonService.getEducationResults(), this.educationResults);
    load(this.commonService.reportingManagers(), this.reportingManagers);
  }

  private loadEmployeeList(): void {
    this.employeeService.getAll().subscribe({
      next: (res) => this.employees.set(res.data ?? []),
      error: (error) => this.setError(error),
    });
  }

  private loadEmployeeDetail(id: number): void {
    this.employeeService.getById(id).subscribe({
      next: (res) => {
        this.selectedEmployee.set(res.data);
        this.selectedEmployeeId.set(id);
        this.patchModel(res.data);
        this.mode.set('view');
      },
      error: (error) => this.setError(error),
    });
  }

  /**
   * Translate the API DTO into a plain EmployeeFormModel and push it into the
   * signal so Signal Forms re-renders all bound fields automatically.
   */
  private patchModel(detail: EmployeeDetailDto): void {
    this.employeeModel.set({
      idClient: detail.idClient ?? 10001001,
      id: detail.id ?? null,
      employeeName: detail.employeeName ?? '',
      employeeNameBangla: detail.employeeNameBangla ?? '',
      employeeImage: detail.employeeImage ?? '',
      fatherName: detail.fatherName ?? '',
      motherName: detail.motherName ?? '',
      idReportingManager: detail.idReportingManager ?? null,
      idJobType: detail.idJobType ?? null,
      idEmployeeType: detail.idEmployeeType ?? null,
      birthDate: this.formatDate(detail.birthDate),
      joiningDate: this.formatDate(detail.joiningDate),
      idGender: detail.idGender ?? null,
      idReligion: detail.idReligion ?? null,
      idDepartment: detail.idDepartment ?? null,
      idSection: detail.idSection ?? null,
      idDesignation: detail.idDesignation ?? null,
      hasOvertime: !!detail.hasOvertime,
      hasAttendenceBonus: !!detail.hasAttendenceBonus,
      idWeekOff: detail.idWeekOff ?? null,
      address: detail.address ?? '',
      presentAddress: detail.presentAddress ?? '',
      nationalIdentificationNumber: detail.nationalIdentificationNumber ?? '',
      contactNo: detail.contactNo ?? '',
      idMaritalStatus: detail.idMaritalStatus ?? null,
      isActive: detail.isActive ?? true,
      employeeFamilyInfos: (detail.employeeFamilyInfos ?? []).map((f) => ({
        id: f.id ?? null,
        idGender: f.idGender ?? null,
        idRelationship: f.idRelationship ?? null,
        name: f.name ?? '',
        dateOfBirth: this.formatDate(f.dateOfBirth),
        contactNo: f.contactNo ?? '',
        currentAddress: f.currentAddress ?? '',
        permanentAddress: f.permanentAddress ?? '',
      })),
      employeeEducationInfos: (detail.employeeEducationInfos ?? []).map((e) => ({
        id: e.id ?? null,
        idEducationLevel: e.idEducationLevel ?? null,
        idEducationExamination: e.idEducationExamination ?? null,
        idEducationResult: e.idEducationResult ?? null,
        major: e.major ?? '',
        passingYear: e.passingYear ?? null,
        instituteName: e.instituteName ?? '',
        cgpa: e.cgpa ?? null,
        examScale: e.examScale ?? null,
        marks: e.marks ?? null,
        isForeignInstitute: !!e.isForeignInstitute,
        duration: e.duration ?? null,
        achievement: e.achievement ?? '',
      })),
      employeeDocuments: (detail.employeeDocuments ?? []).map((d) => ({
        id: d.id ?? null,
        documentName: d.documentName ?? '',
        fileName: d.fileName ?? '',
        uploadDate: this.formatDate(d.uploadDate) ?? new Date().toISOString().slice(0, 10),
        uploadedFileExtention: d.uploadedFileExtention ?? '',
        uploadedFile: d.uploadedFile ?? '',
      })),
      employeeProfessionalCertifications: (detail.employeeProfessionalCertifications ?? []).map((c) => ({
        id: c.id ?? null,
        certificationTitle: c.certificationTitle ?? '',
        certificationInstitute: c.certificationInstitute ?? '',
        instituteLocation: c.instituteLocation ?? '',
        fromDate: this.formatDate(c.fromDate),
        toDate: this.formatDate(c.toDate),
      })),
    });

    this.previewImage.set(this.toPreviewImage(detail.employeeImage));
  }

  private resetFormView(): void {
    this.employeeModel.set(blankEmployeeModel());
    this.mode.set('view');
    this.selectedEmployee.set(null);
    this.selectedEmployeeId.set(null);
    this.previewImage.set('');
    this.clearError();
  }

  /**
   * Build the payload from the model signal's current value.
   * No getRawValue() needed — just read the signal directly.
   */
  private buildPayload(includeId: boolean): EmployeeDetailDto {
    const m = this.employeeModel();
    const payload: any = {
      idClient: m.idClient,
      employeeName: m.employeeName,
      employeeNameBangla: m.employeeNameBangla,
      employeeImage: m.employeeImage || null,
      fatherName: m.fatherName,
      motherName: m.motherName,
      idReportingManager: m.idReportingManager,
      idJobType: m.idJobType,
      idEmployeeType: m.idEmployeeType,
      birthDate: m.birthDate || null,
      joiningDate: m.joiningDate || null,
      idGender: m.idGender,
      idReligion: m.idReligion,
      idDepartment: m.idDepartment,
      idSection: m.idSection,
      idDesignation: m.idDesignation,
      hasOvertime: !!m.hasOvertime,
      hasAttendenceBonus: !!m.hasAttendenceBonus,
      idWeekOff: m.idWeekOff,
      address: m.address,
      presentAddress: m.presentAddress,
      nationalIdentificationNumber: m.nationalIdentificationNumber,
      contactNo: m.contactNo,
      idMaritalStatus: m.idMaritalStatus,
      isActive: !!m.isActive,
      employeeFamilyInfos: m.employeeFamilyInfos.filter(
        (i) => i.name || i.idGender || i.idRelationship,
      ),
      employeeEducationInfos: m.employeeEducationInfos.filter(
        (i) => i.major || i.instituteName || i.idEducationLevel,
      ),
      employeeDocuments: m.employeeDocuments.filter(
        (i) => i.documentName || i.uploadedFile,
      ),
      employeeProfessionalCertifications: m.employeeProfessionalCertifications.filter(
        (i) => i.certificationTitle || i.certificationInstitute,
      ),
    };

    if (includeId) {
      payload.id = m.id;
    }

    return payload;
  }

  /**
   * Signal Forms does not have a markAllAsTouched() equivalent yet.
   * Touch each leaf field individually so the template can show errors.
   */
  private touchAll(): void {
    const f = this.employeeForm;

    const topLevel: Array<FieldState<string | number | null, string>> = [
      f.employeeName(),
      f.employeeNameBangla(),
      f.idDepartment(),
      f.idSection(),
      f.fatherName(),
      f.motherName(),
      f.address(),
      f.presentAddress(),
      f.nationalIdentificationNumber(),
      f.contactNo(),
    ];
    topLevel.forEach((field) => field.markAsTouched());

    const m = this.employeeModel();

    m.employeeFamilyInfos.forEach((_, i) => {
      const item = f.employeeFamilyInfos[i];
      [item.idGender(), item.idRelationship(), item.name()].forEach((field) =>
        field.markAsTouched(),
      );
    });

    m.employeeEducationInfos.forEach((_, i) => {
      const item = f.employeeEducationInfos[i];
      [
        item.idEducationLevel(),
        item.idEducationExamination(),
        item.idEducationResult(),
        item.major(),
        item.passingYear(),
        item.instituteName(),
      ].forEach((field) => field.markAsTouched());
    });

    m.employeeDocuments.forEach((_, i) => {
      const item = f.employeeDocuments[i];
      [item.documentName(), item.fileName(), item.uploadDate()].forEach((field) =>
        field.markAsTouched(),
      );
    });

    m.employeeProfessionalCertifications.forEach((_, i) => {
      const item = f.employeeProfessionalCertifications[i];
      [
        item.certificationTitle(),
        item.certificationInstitute(),
        item.instituteLocation(),
        item.fromDate(),
      ].forEach((field) => field.markAsTouched());
    });
  }

  private readFileAsBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => {
        const result = reader.result as string;
        resolve(result.split(',')[1] ?? '');
      };
      reader.onerror = () => reject(reader.error);
      reader.readAsDataURL(file);
    });
  }

  private formatDate(value?: string): string | null {
    if (!value) return null;
    const parsed = new Date(value);
    return Number.isNaN(parsed.getTime())
      ? null
      : parsed.toLocaleDateString('en-CA', { timeZone: 'Asia/Dhaka' }).slice(0, 10);
  }

  private toPreviewImage(value?: string | null): string {
    if (!value) return '';
    return value.startsWith('data:') ? value : `data:image/png;base64,${value}`;
  }

  private setError(error: any): void {
    this.errorMessage.set(error.error?.message ?? 'An unexpected error occurred.');
  }

  private clearError(): void {
    this.errorMessage.set(null);
  }

  private showMessage(msg: string, type: 'success' | 'warning' | 'danger' = 'success'): void {
    this.message.set(msg);
    this.messageType.set(type);
    setTimeout(() => {
      this.message.set(null);
      this.messageType.set(null);
    }, 3000);
  }
}