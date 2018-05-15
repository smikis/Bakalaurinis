import { Injectable, Output, EventEmitter, OnInit } from '@angular/core';
import { RequestOptionsArgs } from '@angular/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  getAuthenticatedUser(): AuthenticatedUser | null {
    console.log(this.lastAuthenticatedUser);
    return this.lastAuthenticatedUser;
  }

  constructor(private http: HttpClient, private url: UrlService, private router : Router) {
    this.accessToken = localStorage.getItem('accessToken');

    this.getUser().subscribe(data=> {
      this.lastAuthenticatedUser = new AuthenticatedUser(data.email, data.id,data.firstName, data.lastName,data.roles);
      console.log(this.lastAuthenticatedUser);  
      this.accessToken = data.token;
        this.userSubscription.next(this.lastAuthenticatedUser);    
        window.localStorage.setItem('accessToken', this.accessToken);
    },
  error=> {
    window.localStorage.removeItem('accessToken');
    this.router.navigate(['login']);
  });
  }

  getUser() {
    let baseUrl = this.url.getApiUrl(Endpoints.login);
    return this.http.get<LoginResult>(baseUrl, {headers: this.applyAuthentication()});
  }

  async login(login: Login) {       
        var response  = await this.http.post<LoginResult>(this.url.getApiUrl(Endpoints.login), login).toPromise();
        this.lastAuthenticatedUser = new AuthenticatedUser(response.email, response.id,response.firstName, response.lastName,response.roles);
        this.accessToken = response.token;
        this.userSubscription.next(this.lastAuthenticatedUser);    
        window.localStorage.setItem('accessToken', this.accessToken);
        return response;
  }

  logout(){
    window.localStorage.removeItem('accessToken');
    this.router.navigate(['login']);
  }

  applyAuthentication(requestOptions?: RequestOptionsArgs | undefined) : HttpHeaders | undefined {

    if(this.accessToken === null) {
      return void 0;
    } 
    
    return new HttpHeaders().set('authorization', 'Bearer ' + this.accessToken);
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

export interface LoginResult {
  token: string;
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  roles: string[];
}