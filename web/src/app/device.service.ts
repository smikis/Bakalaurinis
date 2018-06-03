import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService, Endpoints } from './url.service';
import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { RequestOptions,ResponseContentType, Headers } from '@angular/http';

@Injectable()
export class DeviceService {

  constructor(private http: HttpClient, private url: UrlService, private login: LoginService) { }

createDevice(device: Device) {
  let baseUrl = this.url.getApiUrl(Endpoints.devices);
  return this.http.post(baseUrl, device, {headers: this.login.applyAuthentication()});
}

updateDevice(device: Device, deviceId: number) {
  let baseUrl = this.url.getApiUrl(Endpoints.getDevice(deviceId));
  return this.http.put(baseUrl, device, {headers: this.login.applyAuthentication()});
}

getDevices() : Observable<Device[]> {
  let baseUrl = this.url.getApiUrl(Endpoints.devices);
  return this.http.get<Device[]>(baseUrl, {headers: this.login.applyAuthentication()});
}

}

export class Device {
  name: string;
  manufacturer: string;
  macAddress: string;
  description: string;
  internetUserId: number;
  warrantyExpiration: Date;
}
