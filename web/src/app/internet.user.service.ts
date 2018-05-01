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

  getInternetUser(id: number) {
    let baseUrl = this.url.getApiUrl(Endpoints.getInternetUser(id));
    return this.http.get<InternetUser>(baseUrl);
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
}
