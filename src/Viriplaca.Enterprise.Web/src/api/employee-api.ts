import type { Guid } from '@/models/guid';
import type { MaritalStatus } from '@/models/organization/marital-status';
import type { Sex } from '@/models/organization/sex';
import type { PageQuery, PageResult } from '@/models/page';
import httpClient from './http-client';

export interface GetEmployeesQuery extends PageQuery {
  departmentId?: Guid;
  jobId?: Guid;
}

export interface EmployeeRow {
  id: Guid;
  name: string;
  departmentName: string;
  jobTitle: string;
}

export interface Employee {
  id: Guid;
  firstName: string;
  lastName?: string;
  imageId?: Guid;
  birthDate: Date;
  sex: Sex;
  maritalStatus: MaritalStatus;
  departmentId: Guid;
  jobId: Guid;
}

export default {
  getList: (query: GetEmployeesQuery) =>
    httpClient.get<PageResult<EmployeeRow>>('/Employees', { params: query }),
  get: (id: Guid) => httpClient.get<Employee>(`/Employees/${id}`),
  create: (employee: Employee) => httpClient.post('/Employees', employee),
  update: (employee: Employee) =>
    httpClient.put(`/Employees/${employee.id}`, employee),
};
