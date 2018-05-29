import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService, Endpoints } from './url.service';
import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { RequestOptions,ResponseContentType, Headers } from '@angular/http';

@Injectable()
export class TimeSpentService {

  constructor(private http: HttpClient, private url: UrlService, private login: LoginService) {
  }

  getProblemTimeSpent(id: number) : Observable<TimeSpent[]> {
    let baseUrl = this.url.getApiUrl(Endpoints.getProblemTimeSpent(id));
    return this.http.get<TimeSpent[]>(baseUrl, {headers: this.login.applyAuthentication()});
  }

  getUserTimeSpentPeriod(userId: string, date:string) : Observable<TimeSpentCalendar[]> {
    let baseUrl = this.url.getApiUrl(Endpoints.getUserTimeSpentPeriod(userId, date));
    return this.http.get<TimeSpentCalendar[]>(baseUrl, {headers: this.login.applyAuthentication()});
  }

  createTimeSpent(model: TimeSpent){
    let baseUrl = this.url.getApiUrl(Endpoints.timeSpent);
    var createTimeSpent = new CreateTimeSpent();
    createTimeSpent.problemId = model.problemId;
    createTimeSpent.userId = model.userId;
    createTimeSpent.description = model.description;
    createTimeSpent.hoursSpent = model.hoursSpent;
    createTimeSpent.dateRecorded = model.dateRecorded.toDateString();
    return this.http.post<any>(baseUrl, createTimeSpent, {headers: this.login.applyAuthentication()});
  }

  update(model: TimeSpent, timeSpentId: number) : Observable<TimeSpent> {
    let baseUrl = this.url.getApiUrl(Endpoints.getTimeSpent(timeSpentId));
    var createTimeSpent = new UpdateTimeSpent();
    createTimeSpent.description = model.description;
    createTimeSpent.hoursSpent = model.hoursSpent;
    createTimeSpent.dateRecorded = model.dateRecorded.toDateString();
    return this.http.put<TimeSpent>(baseUrl, createTimeSpent, {headers: this.login.applyAuthentication()});
  }

  delete(timeSpentId: number) {
    let baseUrl = this.url.getApiUrl(Endpoints.getTimeSpent(timeSpentId));
    return this.http.delete<TimeSpent>(baseUrl, {headers: this.login.applyAuthentication()});
  }


}

export interface TimeSpentCalendar {
  id:           number;
  problemId:    number;
  hoursSpent:   number;
  description:  string;
  problemName:  string;
  dateRecorded: Date;
}

export class TimeSpent {
  id: number;
  hoursSpent: number;
  description: string;
  userId: string;
  problemId: number;
  firstName: string;
  lastName: string;
  dateRecorded: Date;
}

export class UpdateTimeSpent {
  hoursSpent: number;
  description: string;
  dateRecorded: string;  
}

export class CreateTimeSpent {
    hoursSpent: number;
    description: string;
    userId: string;
    problemId: number;
    dateRecorded: string;  
}
