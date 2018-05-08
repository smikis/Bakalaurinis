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
    return this.http.get<TimeSpent[]>(baseUrl);
  }

  createTimeSpent(model: CreateTimeSpent) : Observable<TimeSpent> {
    let baseUrl = this.url.getApiUrl(Endpoints.timeSpent);
    return this.http.post<TimeSpent>(baseUrl, model);
  }

  update(model: TimeSpent, timeSpentId: number) : Observable<TimeSpent> {
    let baseUrl = this.url.getApiUrl(Endpoints.getTimeSpent(timeSpentId));
    return this.http.put<TimeSpent>(baseUrl, model);
  }

  delete(timeSpentId: number) {
    let baseUrl = this.url.getApiUrl(Endpoints.getTimeSpent(timeSpentId));
    return this.http.delete<TimeSpent>(baseUrl);
  }


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

export class CreateTimeSpent {
    hoursSpent: number;
    description: string;
    userId: string;
    problemId: number;  
}
