import { Component, AfterViewInit, OnInit } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { LoginService, AuthenticatedUser } from './login.service';
import { Subscription } from 'rxjs';
import { CreateProblemDialogComponent } from './create-problem-dialog/create-problem-dialog.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit, OnInit {
  constructor(private login: LoginService, private router: Router, private dialog: MatDialog) {

  }
  private authSub: Subscription | null;
  user: AuthenticatedUser | null = null;
  message;
  OneSignal;


  menuExpanded = false;

  ngOnInit() {
    this.OneSignal = window['OneSignal'] || [];
    console.log('Init OneSignal');
    this.OneSignal.push(['init', {
      appId: '3c1aaa0e-b87c-4392-9b04-226e3c465c70',
      autoRegister: true,
      allowLocalhostAsSecureOrigin: true,
      notifyButton: {
        enable: false
      }
    }]);
    console.log('OneSignal Initialized');

    this.OneSignal.push(['registerForPushNotifications']);
    this.OneSignal.push(['sendTag', 'user_id', this.user.id]);

  }
  ngAfterViewInit() {
    this.authSub = this.login.userSubscription.subscribe(result => {
      if (result === null || result === undefined) {
        this.user = null;
      } else {
        this.user = result;
        try {
          console.log(this.OneSignal);
          this.OneSignal.push(['sendTag', 'user_id', result.id]);
        } catch (e) {
          console.log(e);
        }

      }
    });
  }

  logout() {
    this.user = null;
    try {
      this.OneSignal.push(function () {
        this.OneSignal.deleteTag('user_id');
      });
    } catch (e) {
      console.log(e);
    }
    this.login.logout();
  }

  onPlusClick() {
    const createproblemdialogRef = this.dialog.open(CreateProblemDialogComponent, {
      width: '30%',
    });
    createproblemdialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);
      }
    });
  }

}
