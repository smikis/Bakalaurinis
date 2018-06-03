import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {InternetUserService, CreateInternetUser, InternetUser} from '../internet.user.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {Router} from "@angular/router";

@Component({
  selector: 'app-update-internetuser-dialog',
  templateUrl: './update-internetuser-dialog.component.html',
  styleUrls: ['./update-internetuser-dialog.component.scss'],
  providers: [InternetUserService]
})
export class UpdateInternetuserDialogComponent implements OnInit {
 
  myform: FormGroup;
  constructor(private internetUserService: InternetUserService,  private router: Router,
    public dialogRef: MatDialogRef<UpdateInternetuserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    console.log(this.data);
    this.myform = new FormGroup({
      firstName: new FormControl(this.data.row.firstName, Validators.required),
      lastName: new FormControl(this.data.row.lastName, Validators.required),
      description: new FormControl(this.data.row.description, Validators.required),
      location: new FormControl(this.data.row.location, Validators.required),
      ipAddress: new FormControl(this.data.row.ipAddress,Validators.pattern('(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)')),
      internetPlan: new FormControl(this.data.row.internetPlan),
      contract: new FormControl(this.data.row.contractId),
      status: new FormControl(this.data.row.statusId.toString(), Validators.required)
  });
 
  }

  submit() {
    console.log(this.data.row.id);

    var internetUser = new CreateInternetUser();

    internetUser.firstName = this.myform.controls['firstName'].value;
    internetUser.lastName = this.myform.controls['lastName'].value;
    internetUser.description = this.myform.controls['description'].value;
    internetUser.location = this.myform.controls['location'].value;
    internetUser.ipAddress = this.myform.controls['ipAddress'].value;
    internetUser.internetPlan = this.myform.controls['internetPlan'].value;
    internetUser.statusId = this.myform.controls['status'].value;
    internetUser.contractId = this.myform.controls['contract'].value;

    console.log(internetUser);

    this.internetUserService.updateInternetUser(internetUser,this.data.row.id).subscribe(result=> {
      this.dialogRef.close('Success');
    })
  }

}
