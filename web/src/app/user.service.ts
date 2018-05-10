import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService, Endpoints } from './url.service';
import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { RequestOptions,ResponseContentType, Headers } from '@angular/http';

@Injectable()
export class UserService {

  constructor(private http: HttpClient, private url: UrlService, private login: LoginService) {
  }

  searchUsers(searchQuery: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.searchUsers(searchQuery));
    return this.http.get<User[]>(baseUrl);
  }

}

export interface UsersPage {
    total : number;
    data : User[];
}
   

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
}
