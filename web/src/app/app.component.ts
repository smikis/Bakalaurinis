import { Component, AfterViewInit } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { LoginService, AuthenticatedUser } from './login.service';
import { Subscription } from 'rxjs/Subscription';
import {CreateProblemDialogComponent} from './create-problem-dialog/create-problem-dialog.component';
import { MatDialog, MatDialogRef } from '@angular/material';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit {
  private authSub: Subscription | null;
  user: AuthenticatedUser | null;

  constructor(private login: LoginService, private router: Router, private dialog: MatDialog) {
    
  }

menuExpanded = false;
  ngAfterViewInit() {
    this.authSub = this.login.userSubscription.subscribe( result=> {
      if(result === null || result === undefined) {
        this.user = null;
      } else {
        this.user = result;
      }
    });
  }

  logout() {
    this.user = null;
    this.login.logout();
  }

  onPlusClick() {
    let createproblemdialogRef = this.dialog.open(CreateProblemDialogComponent, {
      width: '30%',
    });
    createproblemdialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);       
      }
    });
  }

}
