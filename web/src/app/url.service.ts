import { Injectable } from '@angular/core';

import { environment } from '../environments/environment';


@Injectable()
export class UrlService {

  private readonly baseUrl: string;

  constructor() {
   // this.baseUrl = window.location.protocol + '//' + window.location.host + '/WasteControl.Api/';
   this.baseUrl = "localhost:4200"
  }

  getApiUrl(endpoint: Endpoint) {
    return this.baseUrl + endpoint.value;
  }
}

export interface Endpoint { readonly value: string }

export class Endpoints {
  static readonly login: Endpoint = { value: 'api/Login' };

}
