import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { LoginService, AuthenticatedUser } from '../login.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  private authSub: Subscription | null;
  user: AuthenticatedUser | null = null;
  status = 'Sukurtas';
  constructor(private login: LoginService) {
    this.authSub = this.login.userSubscription.subscribe(result => {
      if (result === null || result === undefined) {
        this.user = null;
      } else {
        this.user = result;
      }
    });
  }

  pageSize = 5;

  @ViewChild(MatPaginator) paginator: MatPaginator;


  ngOnInit() {
  }

}
