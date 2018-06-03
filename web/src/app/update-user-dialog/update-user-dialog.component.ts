import { Component, OnInit,Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {UserService, CreateUser, UpdateUser} from '../user.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {Router} from "@angular/router";

@Component({
  selector: 'app-update-user-dialog',
  templateUrl: './update-user-dialog.component.html',
  styleUrls: ['./update-user-dialog.component.scss'],
  providers: [UserService]
})
export class UpdateUserDialogComponent implements OnInit {

  myform: FormGroup;
  constructor(private userService: UserService,  private router: Router,
    public dialogRef: MatDialogRef<UpdateUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    console.log(this.data);
    this.myform = new FormGroup({
      firstName: new FormControl(this.data.row.name, Validators.required),
      lastName: new FormControl(this.data.row.lastName, Validators.required),
      address: new FormControl(this.data.row.address, Validators.required),
      email: new FormControl(this.data.row.email, [Validators.email, Validators.required])
  });
  }

  submit() {
    var user = new UpdateUser();

    user.name = this.myform.controls['firstName'].value;
    user.lastName = this.myform.controls['lastName'].value;
    user.address = this.myform.controls['address'].value;
    user.email = this.myform.controls['email'].value;

    console.log(user);
    this.userService.updateUser(user, this.data.row.id).subscribe(result=> {
      this.dialogRef.close('Success');
    })
  }

}
