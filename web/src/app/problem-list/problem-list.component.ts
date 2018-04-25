import { Component, OnInit, ViewChild, ElementRef, EventEmitter } from '@angular/core';
import {MatPaginator, PageEvent, MatSelectChange} from '@angular/material';
import { FormGroup, FormControl } from '@angular/forms';
import { DataSource} from '@angular/cdk/collections';
import {ProblemService, Problem} from '../problem.service';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import "rxjs/add/operator/debounceTime";
import { Subject } from "rxjs/Subject";
import "rxjs/add/operator/distinctUntilChanged";
@Component({
  selector: 'app-problem-list',
  templateUrl: './problem-list.component.html',
  styleUrls: ['./problem-list.component.css'],
  providers:[ProblemService]
})
export class ProblemListComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;

  searchChangeEmitter: EventEmitter<any> = new EventEmitter<any>();
  isLoadingResults = true;
  resultsLength = 0;
  pageSize = 10;
  displayedColumns = ['id', 'name', 'description', 'location', 'categoryName', 'assignedUserEmail', 'status'];
  dataSource : ProblemsDataSource;
  readonly updateForm : FormGroup;

  statuses = [
    {value: 'Created', viewValue: 'Created'},
    {value: 'Completed', viewValue: 'Completed'}
  ];

  categories = [
    {value: 'First', viewValue: 'First'},
    {value: 'Second', viewValue: 'Second'}
  ];

  private searchUpdated: Subject<string> = new Subject<string>();

  constructor(private problemService: ProblemService) { 
    this.problemService.getPage(1,this.pageSize).subscribe(data=> {

      this.dataSource = new ProblemsDataSource(this.problemService, data);
      this.isLoadingResults = false;
    })

    this.updateForm = new FormGroup({
      selectedDateFrom: new FormControl(''),
      selectedDateTo: new FormControl(''),
      category: new FormControl(''),
      search: new FormControl(''),
      problemStatus: new FormControl('')
    });

    this.searchChangeEmitter = <any>this.searchUpdated.asObservable()
    .debounceTime(1000)
     .distinctUntilChanged()
     .subscribe(result=> {
      this.getFilteredData();
     });

  }

  ngOnInit() {
  }

  
  applyFilter(value: string) {
    this.searchUpdated.next(value);
  }

  changeCtagory(event: MatSelectChange) {
    this.getFilteredData();
    console.log(event);
  }

  getFilteredData() {
    console.log(this.updateForm.value);
    var dateFrom = <Date>this.updateForm.controls['selectedDateFrom'].value;
    var dateTo = <Date>this.updateForm.controls['selectedDateTo'].value;
    var category = <string>this.updateForm.controls['category'].value;
    var status = <string>this.updateForm.controls['problemStatus'].value;
    var search = <string>this.updateForm.controls['search'].value;
    this.dataSource.loadPage(1, this.pageSize, dateFrom, dateTo, category, status, search);
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
  constructor(private reportService: ProblemService, problems: Problem[]) {
    super();
    this.length = length;
    this.subject = new BehaviorSubject<Problem[]>(problems);
  }
  connect(): Observable<Problem[]> {
    return this.subject.asObservable();
  }
  loadPage(page:number, pageSize:number, start?: Date | undefined, end?: Date | undefined, category?: string | undefined, status?: string | undefined, search?: string | undefined){
    this.reportService.getPage(page,pageSize,start,end,category,status,search).subscribe(data=> {
    this.subject.next(data);
  })
}
  disconnect() {}
}
