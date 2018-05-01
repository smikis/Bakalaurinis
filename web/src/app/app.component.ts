import { Component, AfterViewInit } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { LoginService, AuthenticatedUser } from './login.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit {
  private authSub: Subscription | null;
  user: AuthenticatedUser | null;

  constructor(private login: LoginService, private router: Router) {
    
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

}
