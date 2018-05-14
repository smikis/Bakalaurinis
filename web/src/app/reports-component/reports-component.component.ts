import { Component, OnInit } from '@angular/core';
import {ReportsService, UserReport, TimeConsumingInternetUsers,TimeConsumingProblems} from '../reports.service';
import { MatTableDataSource } from '@angular/material';
import {Router} from "@angular/router";
@Component({
  selector: 'app-reports-component',
  templateUrl: './reports-component.component.html',
  styleUrls: ['./reports-component.component.scss'],
  providers: [ReportsService]
})
export class ReportsComponentComponent implements OnInit {

  userReportsDataSource = new MatTableDataSource();
  userReportsdisplayedColumns = ['firstName', 'lastName', 'time', 'averageTaskTime', 'maxTime', 'minTime', 'problemsSolved'];

  costlyProblemsDataSource = new MatTableDataSource();
  costlyProblemsdisplayedColumns = ['id', 'name', 'timeSpent'];

  costlyUsersDataSource = new MatTableDataSource();
  costlyUsersdisplayedColumns = ['internetUserId', 'firstName','lastName', 'timeSpent'];
  
  constructor(private reportsService: ReportsService,
    private router: Router) { }

  ngOnInit() {
    this.reportsService.getUserReports('2018-01-01', '2018-09-01').subscribe(result=> {
      this.userReportsDataSource.data = result;     
    });

    this.reportsService.getCostlyInternetUsers('2018-01-01', '2018-09-01').subscribe(result=> {
      this.costlyUsersDataSource.data = result;
      console.log(this.costlyUsersDataSource.data);
    });

    this.reportsService.getCostlyProblems('2018-01-01', '2018-09-01').subscribe(result=> {
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

}
