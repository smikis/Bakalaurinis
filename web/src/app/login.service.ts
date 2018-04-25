import { Injectable, Output, EventEmitter, OnInit } from '@angular/core';
import { Http, RequestOptionsArgs, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { UrlService, Endpoints } from './url.service';
import { Subject } from 'rxjs/Subject';

import 'rxjs/add/operator/share';

@Injectable()
export class LoginService {

  private accessToken: string | null;
  @Output() userAuthenticated : EventEmitter<AuthenticatedUser| null>;
  getAccessToken(): string | null {
    return this.accessToken;
  }

  constructor(private http: Http, private url: UrlService) {
    this.userAuthenticated = new EventEmitter<AuthenticatedUser | null>();
    this.accessToken = localStorage.getItem('accessToken');
    if(this.accessToken) {
        console.log("Token exists");
    }
  }


  login(login: Login) {
    this.http.post(this.url.getApiUrl(Endpoints.login), login).subscribe(result=> {
        console.log(result);
        var lastAuthenticatedUser = new AuthenticatedUser(<string>result.json().email, <string[]>result.json().roles);
        this.userAuthenticated.emit(lastAuthenticatedUser);
        this.accessToken = <string>result.json().token;
        window.localStorage.setItem('accessToken', this.accessToken);
    });
  }

  logout(){
  
  }

  applyAuthentication(requestOptions?: RequestOptionsArgs | undefined) : RequestOptionsArgs | undefined {

    if(this.accessToken === null) {
      return void 0;
    } 
    
    const req : RequestOptionsArgs =
    requestOptions === void 0 ? {  } :  requestOptions;

    if(!req.headers) {
      req.headers = new Headers();
    }
    req.headers.set('Authorization', 'Bearer ' + this.accessToken);

    return req;
  }
}

export class Login {
    email: string;
    password: string;
}

export class AuthenticatedUser {
    constructor(public readonly email: string, public readonly roles: string[]) {       
    }

    isInRole(role: string) : boolean {
        if (this.roles.indexOf(role) > -1) {
           return true;
        } else {
            return false;
        }
    }
}