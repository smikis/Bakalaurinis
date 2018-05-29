import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService, Endpoints } from './url.service';
import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { RequestOptions,ResponseContentType, Headers } from '@angular/http';

@Injectable()
export class ReportsService {

  constructor(private http: HttpClient, private url: UrlService, private login: LoginService) {
  }


  getUserReports(dateFrom: string, dateTo:string) : Observable<UserReport[]> {
    let baseUrl = this.url.getApiUrl(Endpoints.getUserReports(dateFrom, dateTo));
    return this.http.get<UserReport[]>(baseUrl, {headers: this.login.applyAuthentication()});
  }

  getCostlyInternetUsers(dateFrom: string, dateTo:string) : Observable<TimeConsumingInternetUsers[]> {
    let baseUrl = this.url.getApiUrl(Endpoints.getTimeConsumingUsers(dateFrom, dateTo));
    return this.http.get<TimeConsumingInternetUsers[]>(baseUrl, {headers: this.login.applyAuthentication()});
  }

  getCostlyProblems(dateFrom: string, dateTo:string) : Observable<TimeConsumingProblems[]> {
    let baseUrl = this.url.getApiUrl(Endpoints.getTimeConsumingProblems(dateFrom, dateTo));
    return this.http.get<TimeConsumingProblems[]>(baseUrl, {headers: this.login.applyAuthentication()});
  }


}

export interface UserReport {
    timeSpentCount:  number;
    problemsSolved:  number;
    time:            number;
    maxTime:         number;
    minTime:         number;
    averageTaskTime: number;
    firstName:       string;
    lastName:        string;
    userId:          string;
}

export interface TimeConsumingInternetUsers {
    internetUserId: number;
    firstName:      string;
    lastName:       string;
    timeSpent:      number;
}

export interface TimeConsumingProblems {
    id:        number;
    name:      string;
    timeSpent: number;
}



