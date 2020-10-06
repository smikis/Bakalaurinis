import { Component, OnInit } from '@angular/core';
import {ReportsService, UserReport, TimeConsumingInternetUsers, TimeConsumingProblems} from '../reports.service';
import { MatTableDataSource } from '@angular/material/table';
import {Router} from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
@Component({
  selector: 'app-reports-component',
  templateUrl: './reports-component.component.html',
  styleUrls: ['./reports-component.component.scss'],
  providers: [ReportsService]
})
export class ReportsComponentComponent implements OnInit {
  readonly updateForm: FormGroup;

  userReportsDataSource = new MatTableDataSource();
  userReportsdisplayedColumns = ['firstName', 'lastName', 'time', 'averageTaskTime', 'maxTime', 'minTime', 'problemsSolved'];

  costlyProblemsDataSource = new MatTableDataSource();
  costlyProblemsdisplayedColumns = ['id', 'name', 'timeSpent'];

  costlyUsersDataSource = new MatTableDataSource();
  costlyUsersdisplayedColumns = ['internetUserId', 'firstName', 'lastName', 'timeSpent'];

  constructor(private reportsService: ReportsService,
    private router: Router) {
      this.updateForm = new FormGroup({
        selectedDateFrom: new FormControl(''),
        selectedDateTo: new FormControl('')
      });
    }

  ngOnInit() {
    const date = new Date();
    const firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
    const lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    this.updateForm.controls['selectedDateFrom'].setValue(firstDay);
    this.updateForm.controls['selectedDateTo'].setValue(lastDay);
    this.updateData(firstDay, lastDay);

  }

  updateData(firstDay: Date, lastDay: Date) {
    this.reportsService.getUserReports(firstDay.toISOString(), lastDay.toISOString()).subscribe(result => {
      this.userReportsDataSource.data = result;
    });

    this.reportsService.getCostlyInternetUsers(firstDay.toISOString(), lastDay.toISOString()).subscribe(result => {
      this.costlyUsersDataSource.data = result;
      console.log(this.costlyUsersDataSource.data);
    });

    this.reportsService.getCostlyProblems(firstDay.toISOString(), lastDay.toISOString()).subscribe(result => {
      this.costlyProblemsDataSource.data = result;
      console.log(this.costlyProblemsDataSource.data);
    });
  }

  selectInternetUser(row: any) {
    this.router.navigate(['/internetUser', row.internetUserId]);
  }

  selectProblem(row: any) {
    this.router.navigate(['/problem', row.id]);
  }

  selectUser(row: any) {
    this.router.navigate(['/systemUser', row.userId]);
  }

  dateChanged() {
    const dateFrom = <Date>this.updateForm.controls['selectedDateFrom'].value;
    const dateTo = <Date>this.updateForm.controls['selectedDateTo'].value;

    if (dateTo > dateFrom) {
      this.updateData(dateFrom, dateTo);
    }
  }

}
