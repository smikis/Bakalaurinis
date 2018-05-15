import { Component, OnInit, ViewChild, Input } from '@angular/core';
import {MatPaginator, PageEvent, MatSelectChange} from '@angular/material';
import { FormGroup, FormControl } from '@angular/forms';
import { DataSource} from '@angular/cdk/collections';
import {ProblemService, Problem} from '../problem.service';
import {Router} from "@angular/router";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
@Component({
  selector: 'app-problem-list-compact',
  templateUrl: './problem-list-compact.component.html',
  styleUrls: ['./problem-list-compact.component.css'],
  providers:[ProblemService]
})
export class ProblemListCompactComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @Input() assignedUser: string;
  @Input() status: string;
  isLoadingResults = true;
  resultsLength = 0;
  pageSize = 5;
  displayedColumns = ['id', 'name', 'assignedUserEmail', 'status'];
  dataSource : ProblemsDataSource;

  constructor(private problemService: ProblemService, private router: Router) { 
    this.problemService.getPage(0,this.pageSize).subscribe(data=> {
      this.resultsLength = data.total;
      this.dataSource = new ProblemsDataSource(this.problemService, data.data, data.total);
      this.isLoadingResults = false;
    });
  }

  ngOnInit() {
    this.paginator.pageIndex = 0;
  }


  selectRow(row: Problem) {
    this.router.navigate(['/problem', row.id]);
  }

  onPaginateChange(event : PageEvent){ 
    this.dataSource.loadPage(event.pageIndex,event.pageSize);
    this.resultsLength = this.dataSource.length;
  }

}

export class ProblemsDataSource extends DataSource<any> {
  length : number;
  problems:  Observable<Problem[]>;
  subject: BehaviorSubject<Problem[]>;
  constructor(private reportService: ProblemService, problems: Problem[], length:number) {
    super();
    this.length = length;
    this.subject = new BehaviorSubject<Problem[]>(problems);
  }
  connect(): Observable<Problem[]> {
    return this.subject.asObservable();
  }
  loadPage(page:number, pageSize:number, assignedUser?: string | undefined, status?: string | undefined){
    this.reportService.getPage(page,pageSize,undefined,undefined,undefined,status,undefined,assignedUser).subscribe(data=> {
    this.subject.next(data.data);
    this.length = data.total;
  })
}
  disconnect() {}
}
