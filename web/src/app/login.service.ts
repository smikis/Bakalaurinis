import { Injectable, Output, EventEmitter, OnInit } from '@angular/core';
import { Http, RequestOptionsArgs, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { UrlService, Endpoints } from './url.service';
import { Subject } from 'rxjs/Subject';
import { Router } from '@angular/router';
import {BehaviorSubject} from 'rxjs/BehaviorSubject'
import 'rxjs/add/operator/share';

@Injectable()
export class LoginService {

  private accessToken: string | null;
  private lastAuthenticatedUser: AuthenticatedUser | null;
  @Output() userSubscription = new BehaviorSubject<AuthenticatedUser>(null);
  getAccessToken(): string | null {
    return this.accessToken;
  }

  getAuthenticatedUser?(): AuthenticatedUser | null {
    return this.lastAuthenticatedUser;
  }

  constructor(private http: Http, private url: UrlService, private router : Router) {
    this.accessToken = localStorage.getItem('accessToken');
    if(this.accessToken) {
        this.lastAuthenticatedUser = new AuthenticatedUser('test', '17831a88-17b9-4d81-b9e6-6aac5668e6c1', 'Tomas', 'Existing', ['test', 'Admin']);
        this.userSubscription.next(this.lastAuthenticatedUser);
        console.log("Token exists");
    }
  }


  async login(login: Login) {       
        var response  = await this.http.post(this.url.getApiUrl(Endpoints.login), login).toPromise();
        console.log(response);
        this.lastAuthenticatedUser = new AuthenticatedUser(<string>response.json().email, <string>response.json().id, <string>response.json().firstName, <string>response.json().lastName, <string[]>response.json().roles);
        console.log(this.lastAuthenticatedUser);
        this.accessToken = <string>response.json().token;
        this.userSubscription.next(this.lastAuthenticatedUser);    
        window.localStorage.setItem('accessToken', this.accessToken);
        return response.ok;
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
    constructor(public readonly email: string, public readonly id: string, public readonly firstName: string, public readonly lastName: string, public readonly roles: string[]) {       
    }

    isInRole(role: string) : boolean {
        if (this.roles.indexOf(role) > -1) {
           return true;
        } else {
            return false;
        }
    }
}