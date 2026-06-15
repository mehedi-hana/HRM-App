import { Routes } from '@angular/router';
import { EmployeePageComponent } from './components/employee-page/employee-page.component';
import { EmployeePageNewComponent } from './components/employee-page/employee-page-signal-form/employee-page.component.new';


export const routes: Routes = 
[
    {'path': '', 'component': EmployeePageComponent},
    {'path': 'new', 'component': EmployeePageNewComponent},
];