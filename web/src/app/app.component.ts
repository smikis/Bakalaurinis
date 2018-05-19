import { Component, AfterViewInit, OnInit } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { LoginService, AuthenticatedUser } from './login.service';
import { Subscription } from 'rxjs/Subscription';
import { CreateProblemDialogComponent } from './create-problem-dialog/create-problem-dialog.component';
import { MatDialog, MatDialogRef } from '@angular/material';
import { MessagingService, User } from "./messaging.service";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit, OnInit {
  private authSub: Subscription | null;
  user: AuthenticatedUser | null = null;
  message;
  OneSignal;
  constructor(private login: LoginService, private router: Router, private dialog: MatDialog) {

  }

  ngOnInit() {
    this.OneSignal = window['OneSignal'] || [];
    console.log("Init OneSignal");
    this.OneSignal.push(["init", {
      appId: "3c1aaa0e-b87c-4392-9b04-226e3c465c70",
      autoRegister: true,
      allowLocalhostAsSecureOrigin: true,
      notifyButton: {
        enable: false
      }
    }]);
    console.log('OneSignal Initialized');

    this.OneSignal.push(function () {     

      this.OneSignal.isPushNotificationsEnabled(function(isEnabled) {
        if (!isEnabled) {
          this.OneSignal.push(["registerForPushNotifications"]);
        }
      });
    
      this.OneSignal.on('notificationDisplay', function(event) {
        console.log(event);
      });

    });
  }


  menuExpanded = false;
  ngAfterViewInit() {
    this.authSub = this.login.userSubscription.subscribe(result => {
      if (result === null || result === undefined) {
        this.user = null;
      } else {
        this.user = result;
        this.OneSignal.sendTag("user_id", result.id);
      }
    });
  }

  logout() {
    this.user = null;
    this.OneSignal.deleteTag("user_id");
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
