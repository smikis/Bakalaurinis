import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService, Endpoints } from './url.service';
import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { RequestOptions,ResponseContentType, Headers } from '@angular/http';

@Injectable()
export class PingService {

  constructor(private http: HttpClient, private url: UrlService, private login: LoginService) {
  }

  getInternetUserPings(id: number) : Observable<Ping[]> {
    let baseUrl = this.url.getApiUrl(Endpoints.getInternetUserPings(id));
    return this.http.get<Ping[]>(baseUrl);
  }

  getInternetUserPingInformation(id: number) : Observable<PingInformation> {
    let baseUrl = this.url.getApiUrl(Endpoints.getInternetUserPingInformation(id));
    return this.http.get<PingInformation>(baseUrl);
  }

}

export class Ping {
    ipAddress: string;
    time: number;
    recorded: Date;
    status: string;
}

export class PingInformation {
    lastFailDate: Date;
    uptime: number;
}