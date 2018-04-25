import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginService, Login } from '../login.service';
import { Router } from '@angular/router';
import { MatSnackBar, MatSnackBarDismiss } from '@angular/material';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  readonly loginForm : FormGroup;

  busy: boolean;
  authenticationFailed: boolean;
  communicatoinError: boolean;

  constructor(private loginService : LoginService, private router : Router, private snackbar: MatSnackBar) {
    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required])
    });
   }

  ngOnInit() {
  }

  login(): void {
   var login = new Login();
   login.email = <string>this.loginForm.controls['username'].value;
   login.password = <string>this.loginForm.controls['password'].value;
   this.loginService.login(login).then(success=> {
       if(success) {
        this.router.navigate(['dashboard']);
       }     
   }).catch(error=> {
    this.snackbar.open('Neteisingi prisijungimo duomenys', 'Gerai').afterDismissed().subscribe((value: MatSnackBarDismiss) => {
        this.loginForm.controls['password'].reset();
      });
   });
  
  }

}
