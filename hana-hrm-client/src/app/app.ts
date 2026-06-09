import { Component } from '@angular/core';
import { EmployeePageComponent } from './components/employee-page/employee-page.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [EmployeePageComponent],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {}
