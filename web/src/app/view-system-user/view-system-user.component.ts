import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
@Component({
  selector: 'app-view-system-user',
  templateUrl: './view-system-user.component.html',
  styleUrls: ['./view-system-user.component.scss']
})
export class ViewSystemUserComponent implements OnInit {
  userId: string;
  constructor(private route:ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe( params => {
      this.userId = params['id'];
      console.log(this.userId);
    });
  }

}
