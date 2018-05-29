import { Component, OnInit } from '@angular/core';
import { LoginService, AuthenticatedUser } from '../login.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-my-problem-list',
  templateUrl: './my-problem-list.component.html',
  styleUrls: ['./my-problem-list.component.scss']
})
export class MyProblemListComponent implements OnInit {

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

  ngOnInit() {
  }

}
