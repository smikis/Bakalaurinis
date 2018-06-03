import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {UserService, CreateUser} from '../user.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {Router} from "@angular/router";
@Component({
  selector: 'app-create-systemuser-dialog',
  templateUrl: './create-systemuser-dialog.component.html',
  styleUrls: ['./create-systemuser-dialog.component.scss'],
  providers: [UserService]
})
export class CreateSystemuserDialogComponent implements OnInit {

  myform: FormGroup;
  constructor(private userService: UserService,  private router: Router,
    public dialogRef: MatDialogRef<CreateSystemuserDialogComponent>) { }

  ngOnInit() {
    this.myform = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      address: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.email, Validators.required]),
      password: new FormControl('', Validators.required)
  });
  }

  generatePassword() {
   this.myform.controls['password'].setValue(this.randomPassword(8)); 
  }

  randomPassword(length) {
    var chars = "abcdefghijklmnopqrstuvwxyz!@#$%^&*()-+<>ABCDEFGHIJKLMNOP1234567890";
    var pass = "";
    for (var x = 0; x < length; x++) {
        var i = Math.floor(Math.random() * chars.length);
        pass += chars.charAt(i);
    }
    return pass;
}

  submit() {
    var user = new CreateUser();

    user.name = this.myform.controls['firstName'].value;
    user.lastName = this.myform.controls['lastName'].value;
    user.address = this.myform.controls['address'].value;
    user.email = this.myform.controls['email'].value;
    user.password = this.myform.controls['password'].value;

    console.log(user);
    this.userService.createUser(user).subscribe(result=> {
      this.dialogRef.close('Success');
    })
  }

}
