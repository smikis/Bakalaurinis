import { Component, OnInit, ViewChild, ElementRef, EventEmitter } from '@angular/core';
import {MatPaginator, PageEvent, MatSelectChange} from '@angular/material';
import { FormGroup, FormControl } from '@angular/forms';
import { DataSource} from '@angular/cdk/collections';
import {InternetUserService, InternetUser} from '../internet.user.service';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import "rxjs/add/operator/debounceTime";
import { Subject } from "rxjs/Subject";
import "rxjs/add/operator/distinctUntilChanged";
import {Router} from "@angular/router";
import { MatDialog, MatDialogRef } from '@angular/material';
import { CreateInterentuserDialogComponent } from '../create-interentuser-dialog/create-interentuser-dialog.component';
import { UpdateInternetuserDialogComponent } from '../update-internetuser-dialog/update-internetuser-dialog.component';

@Component({
  selector: 'app-internet-user-list',
  templateUrl: './internet-user-list.component.html',
  styleUrls: ['./internet-user-list.component.scss'],
  providers: [InternetUserService]
})
export class InternetUserListComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;

  readonly updateForm : FormGroup;
  searchChangeEmitter: EventEmitter<any> = new EventEmitter<any>();
  isLoadingResults = true;
  resultsLength = 0;
  pageSize = 10;
  displayedColumns = ['id', 'firstName', 'lastName', 'location', 'ipAddress', 'internetPlan', 'actionsColumn'];
  dataSource : UserDataSource;

  private searchUpdated: Subject<string> = new Subject<string>();

  constructor(private internetUserService: InternetUserService, private router: Router,private dialog: MatDialog) {
    this.internetUserService.getPage(0,this.pageSize).subscribe(data=> {
      this.resultsLength = data.total;
      this.dataSource = new UserDataSource(this.internetUserService, data.data, data.total);
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
    var search = <string>this.updateForm.controls['search'].value;   
    this.dataSource.loadPage(event.pageIndex,event.pageSize, search);
  }

  selectRow(row: InternetUser) {
    this.router.navigate(['/internetUser', row.id]);
  }

  createInternetUser() {
    let createproblemdialogRef = this.dialog.open(CreateInterentuserDialogComponent, {
      width: '30%',
    });
    createproblemdialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getFilteredData();
      }
    });
  }

  updateInternetUser(row: InternetUser) {
    console.log(row);
    let updateUserdialogRef = this.dialog.open(UpdateInternetuserDialogComponent, {
      width: '30%',
      data: {row}
    });
    updateUserdialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getFilteredData();
      }
    });
  }

}

export class UserDataSource extends DataSource<any> {
  length : number;
  problems:  Observable<InternetUser[]>;
  subject: BehaviorSubject<InternetUser[]>;
  constructor(private reportService: InternetUserService, problems: InternetUser[], length:number) {
    super();
    this.length = length;
    this.subject = new BehaviorSubject<InternetUser[]>(problems);
  }
  connect(): Observable<InternetUser[]> {
    return this.subject.asObservable();
  }
  loadPage(page:number, pageSize:number, search?: string | undefined){
    this.reportService.getPage(page,pageSize,search).subscribe(data=> {
    this.subject.next(data.data);
    console.log(data.total);
    this.length = data.total;
  })
}
  disconnect() {}
}
