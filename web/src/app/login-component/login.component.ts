import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginService, Login } from '../login.service';
import { Router } from '@angular/router'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [LoginService]
})
export class LoginComponent implements OnInit {

  readonly loginForm : FormGroup;

  busy: boolean;
  authenticationFailed: boolean;
  communicatoinError: boolean;

  constructor(private loginService : LoginService, private router : Router) {
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
   this.loginService.login(login);
   console.log(login);
  }

}
