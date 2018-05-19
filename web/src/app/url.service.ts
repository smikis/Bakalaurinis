import { Injectable } from '@angular/core';

import { environment } from '../environments/environment';


@Injectable()
export class UrlService {

  private readonly baseUrl: string;

  constructor() {
   // this.baseUrl = window.location.protocol + '//' + window.location.host + '/WasteControl.Api/';
  // this.baseUrl = "http://localhost:3442/"
  this.baseUrl = "http://138.68.65.226:3000/";
  }

  getApiUrl(endpoint: Endpoint) {
    return this.baseUrl + endpoint.value;
  }
}

export interface Endpoint { readonly value: string }

export class Endpoints {
  static readonly login: Endpoint = { value: 'api/Login' };
  static readonly problemPage: (page: number, pageSize: number) => Endpoint = (page, pageSize) => { return { value: `api/Problem/filtered/${page}/${pageSize}` }};
  static readonly getProblem: (id: number) => Endpoint = (id) => { return { value: `api/Problem/${id}/` }};
  static readonly problem: Endpoint = { value: 'api/Problem' };
  static readonly internetUser: Endpoint = { value: 'api/InternetUser' };
  static readonly getInternetUser: (id: number) => Endpoint = (id) => { return { value: `api/InternetUser/${id}/` }};
  static readonly searchInternetUsers: (searchQuery: string) => Endpoint = (searchQuery) => { return { value: `api/InternetUser/search/${searchQuery}/` }};
  static readonly internetUsersPage: (page: number, pageSize: number) => Endpoint = (page, pageSize) => { return { value: `api/InternetUser/filtered/${page}/${pageSize}` }};
  static readonly getProblemComments: (problemId: number) => Endpoint = (problemId) => { return { value: `api/Comment/${problemId}/` }};
  static readonly comment: Endpoint = { value: 'api/Comment' };
  static readonly getProblemTimeSpent: (id: number) => Endpoint = (id) => { return { value: `api/TimeSpent/problem/${id}/` }};
  static readonly getUserTimeSpentPeriod: (userId: string, date: string) => Endpoint = (userId, date) => { return { value: `/api/TimeSpent/user/${userId}/${date}` }};
  static readonly getUserTimeSpent: (userId: string) => Endpoint = (userId) => { return { value: `api/TimeSpent/user/${userId}/` }};
  static readonly getTimeSpent: (id: number) => Endpoint = (id) => { return { value: `api/TimeSpent/${id}/` }};
  static readonly timeSpent: Endpoint = { value: 'api/TimeSpent' };
  static readonly getProblemTags: (id: number) => Endpoint = (id) => { return { value: `api/Tag/problem/${id}/` }};
  static readonly getTag: (id: number) => Endpoint = (id) => { return { value: `api/Tag/${id}/` }};
  static readonly tags: Endpoint = { value: 'api/Tag' };
  static readonly searchUsers: (searchQuery: string) => Endpoint = (searchQuery) => { return { value: `api/User/search/${searchQuery}/` }};
  static readonly usersPage: (page: number, pageSize: number) => Endpoint = (page, pageSize) => { return { value: `api/User/filtered/${page}/${pageSize}` }};
  static readonly getInternetUserPings: (internetUserId: number) => Endpoint = (internetUserId) => { return { value: `api/Ping/${internetUserId}/` }};
  static readonly getInternetUserPingInformation: (internetUserId: number) => Endpoint = (internetUserId) => { return { value: `api/Ping/information/${internetUserId}/` }};
  static readonly getInternetUserPingResponseTime: (internetUserId: number) => Endpoint = (internetUserId) => { return { value: `api/Ping/response/${internetUserId}/` }};
  static readonly getUserReports: (dateFrom: string, dateTo: string) => Endpoint = (dateFrom, dateTo) => { return { value: `api/Reports/user/${dateFrom}/${dateTo}` }};
  static readonly getTimeConsumingUsers: (dateFrom: string, dateTo: string) => Endpoint = (dateFrom, dateTo) => { return { value: `api/Reports/internetUser/5/${dateFrom}/${dateTo}` }};
  static readonly getTimeConsumingProblems: (dateFrom: string, dateTo: string) => Endpoint = (dateFrom, dateTo) => { return { value: `api/Reports/problem/5/${dateFrom}/${dateTo}` }};
  static readonly getUserLocation: (userId: string) => Endpoint = (userId) => { return { value: `api/Location/${userId}/` }};
  static readonly getUser: (userId: string) => Endpoint = (userId) => { return { value: `api/User/${userId}/` }};
}
