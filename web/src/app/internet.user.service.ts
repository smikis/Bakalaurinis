import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService, Endpoints } from './url.service';
import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { RequestOptions,ResponseContentType, Headers } from '@angular/http';

@Injectable()
export class InternetUserService {

  constructor(private http: HttpClient, private url: UrlService, private login: LoginService) {
  }

  getAllInternetUsers() {
    let baseUrl = this.url.getApiUrl(Endpoints.internetUser);
    return this.http.get<InternetUser[]>(baseUrl, {headers: this.login.applyAuthentication()});
  }

  createInternetUser(internetUser: CreateInternetUser) {
    let baseUrl = this.url.getApiUrl(Endpoints.internetUser);
    return this.http.post(baseUrl, internetUser, {headers: this.login.applyAuthentication()});
  }

  getPage(page:number, pageSize:number, search?: string | undefined) : Observable<InternetUserPage> {
    let baseUrl = this.url.getApiUrl(Endpoints.internetUsersPage(page,pageSize)) + this.generateArgs(search);
    return this.http.get<InternetUserPage>(baseUrl, {headers: this.login.applyAuthentication()});
  }

  searchInternetUsers(searchQuery: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.searchInternetUsers(searchQuery));
    return this.http.get<InternetUser[]>(baseUrl, {headers: this.login.applyAuthentication()});
  }

  getInternetUser(id: number) {
    let baseUrl = this.url.getApiUrl(Endpoints.getInternetUser(id));
    return this.http.get<InternetUser>(baseUrl, {headers: this.login.applyAuthentication()});
  }

  generateArgs(search?: string | undefined) : string {
    let args ="?";
      if(search !== undefined) {
        args += "&searchTerm=" + search;
      }        
    return args;
  }

}

export interface InternetUserPage {
    total : number;
    data : InternetUser[];
   }
   

export interface InternetUser {
  id: number;
  firstName: string;
  lastName: string;
  description: string;
  location: string;
  ipAddress: string;
  created: Date;
  internetPlan: string;
  statusId: number;
  contractId: number;
}

export class CreateInternetUser {
  firstName: string;
  lastName: string;
  description: string;
  location: string;
  ipAddress: string;
  internetPlan: string;
  statusId: number;
  contractId: number;
}
