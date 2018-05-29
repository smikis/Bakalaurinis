import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {InternetUserService, CreateInternetUser} from '../internet.user.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {Router} from "@angular/router";

@Component({
  selector: 'app-create-interentuser-dialog',
  templateUrl: './create-interentuser-dialog.component.html',
  styleUrls: ['./create-interentuser-dialog.component.scss'],
  providers: [InternetUserService]
})
export class CreateInterentuserDialogComponent implements OnInit {
  myform: FormGroup;
  constructor(private internetUserService: InternetUserService,  private router: Router,
    public dialogRef: MatDialogRef<CreateInterentuserDialogComponent>) { }

  ngOnInit() {
    this.myform = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      location: new FormControl('', Validators.required),
      ipAddress: new FormControl('',Validators.pattern('(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)')),
      internetPlan: new FormControl('')
  });
  }

  submit() {
    var internetUser = new CreateInternetUser();

    internetUser.firstName = this.myform.controls['firstName'].value;
    internetUser.lastName = this.myform.controls['lastName'].value;
    internetUser.description = this.myform.controls['description'].value;
    internetUser.location = this.myform.controls['location'].value;
    internetUser.ipAddress = this.myform.controls['ipAddress'].value;
    internetUser.internetPlan = this.myform.controls['internetPlan'].value;

    console.log(internetUser);
    this.internetUserService.createInternetUser(internetUser).subscribe(result=> {
      this.dialogRef.close('Success');
    })
  }

}
