import { Component, OnInit, ViewChild, ElementRef, EventEmitter } from '@angular/core';
import {MatPaginator, PageEvent, MatSelectChange} from '@angular/material';
import { FormGroup, FormControl } from '@angular/forms';
import { DataSource} from '@angular/cdk/collections';
import {UserService, User} from '../user.service';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import "rxjs/add/operator/debounceTime";
import { Subject } from "rxjs/Subject";
import "rxjs/add/operator/distinctUntilChanged";
import {Router} from "@angular/router";

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss'],
  providers: [UserService]
})
export class UsersListComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;

  readonly updateForm : FormGroup;
  searchChangeEmitter: EventEmitter<any> = new EventEmitter<any>();
  isLoadingResults = true;
  resultsLength = 0;
  pageSize = 10;
  displayedColumns = ['id', 'name', 'lastName', 'email'];
  dataSource : UserDataSource;

  private searchUpdated: Subject<string> = new Subject<string>();

  constructor(private userService: UserService, private router: Router) {
    this.userService.getPage(0,this.pageSize).subscribe(data=> {
      this.resultsLength = data.total;
      this.dataSource = new UserDataSource(this.userService, data.data, data.total);
      this.isLoadingResults = false;
    });

    this.updateForm = new FormGroup({
      search: new FormControl('')
    });

    this.searchChangeEmitter = <any>this.searchUpdated.asObservable()
    .debounceTime(1000)
     .distinctUntilChanged()
     .subscribe(result=> {
      this.getFilteredData();
     });

   }

  ngOnInit() {
    this.paginator.pageIndex = 0;
  }

  getFilteredData() {
    var search = <string>this.updateForm.controls['search'].value;
    this.dataSource.loadPage(0, this.pageSize, search);
  }

  applyFilter(value: string) {
    this.searchUpdated.next(value);
  }

  onPaginateChange(event : PageEvent){
   
    this.dataSource.loadPage(event.pageIndex,event.pageSize);
    this.resultsLength = this.dataSource.length;
  }

}

export class UserDataSource extends DataSource<any> {
  length : number;
  problems:  Observable<User[]>;
  subject: BehaviorSubject<User[]>;
  constructor(private reportService: UserService, problems: User[], length:number) {
    super();
    this.length = length;
    this.subject = new BehaviorSubject<User[]>(problems);
  }
  connect(): Observable<User[]> {
    return this.subject.asObservable();
  }
  loadPage(page:number, pageSize:number, search?: string | undefined){
    this.reportService.getPage(page,pageSize,search).subscribe(data=> {
    this.subject.next(data.data);
    this.length = data.total;
  })
}
  disconnect() {}
}
