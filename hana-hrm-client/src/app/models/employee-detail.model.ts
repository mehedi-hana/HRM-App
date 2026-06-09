export interface EmployeeFamilyInfoDto {
  id?: number;
  idGender: number;
  idRelationship: number;
  name: string;
  dateOfBirth?: string;
  contactNo?: string;
  currentAddress?: string;
  permanentAddress?: string;
}

export interface EmployeeEducationInfoDto {
  id?: number;
  idEducationLevel: number;
  idEducationExamination: number;
  idEducationResult: number;
  major: string;
  passingYear: number;
  instituteName: string;
  cgpa?: number;
  examScale?: number;
  marks?: number;
  isForeignInstitute: boolean;
  duration?: number;
  achievement?: string;
}

export interface EmployeeDocumentDto {
  id?: number;
  documentName: string;
  fileName: string;
  uploadDate: string;
  uploadedFileExtention?: string;
  uploadedFile: string;
}

export interface EmployeeProfessionalCertificationDto {
  id?: number;
  certificationTitle: string;
  certificationInstitute: string;
  instituteLocation: string;
  fromDate: string;
  toDate?: string;
}

export interface EmployeeDetailDto {
  idClient: number;
  id?: number;
  employeeName?: string;
  employeeNameBangla?: string;
  employeeImage?: string | null;
  fatherName?: string;
  motherName?: string;
  idReportingManager?: number;
  idJobType?: number;
  idEmployeeType?: number;
  birthDate?: string;
  joiningDate?: string;
  idGender?: number;
  idReligion?: number;
  idDepartment: number;
  idSection: number;
  idDesignation?: number;
  hasOvertime: boolean;
  hasAttendenceBonus: boolean;
  idWeekOff?: number;
  address?: string;
  presentAddress?: string;
  nationalIdentificationNumber?: string;
  contactNo?: string;
  idMaritalStatus?: number;
  isActive: boolean;
  employeeFamilyInfos: EmployeeFamilyInfoDto[];
  employeeEducationInfos: EmployeeEducationInfoDto[];
  employeeDocuments: EmployeeDocumentDto[];
  employeeProfessionalCertifications: EmployeeProfessionalCertificationDto[];
}
