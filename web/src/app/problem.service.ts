import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService, Endpoints } from './url.service';
import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { RequestOptions,ResponseContentType, Headers } from '@angular/http';

@Injectable()
export class ProblemService {

  constructor(private http: HttpClient, private url: UrlService, private login: LoginService) {
  }

  createProblem(problem: CreateProblem) {
    let baseUrl = this.url.getApiUrl(Endpoints.problem);
    return this.http.post<number>(baseUrl, problem);
  }

  updateProblemInternetUser(problemId: number, internetUserId: number) {
    let baseUrl = this.url.getApiUrl(Endpoints.updateProblemInternetUser(problemId));
    baseUrl += "?internetUserId=" + internetUserId;
    return this.http.put(baseUrl, null);
  }

  updateProblemAssignedUser(problemId: number, userId: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.updateProblemAssignedUser(problemId));
    baseUrl += "?userId=" + userId;
    return this.http.put(baseUrl,null);
  }

  updateProblemDescription(problemId: number, description: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.updateProblemDescription(problemId));
    baseUrl += "?description=" + description;
    return this.http.put(baseUrl,null);
  }

  updateProblemStatus(problemId: number, status: string) {
    let baseUrl = this.url.getApiUrl(Endpoints.updateProblemStatus(problemId));
    baseUrl += "?statusId=" + status;
    return this.http.put(baseUrl,null);
  }

  getProblem(id: number) {
    let baseUrl = this.url.getApiUrl(Endpoints.getProblem(id));
    return this.http.get<Problem>(baseUrl);
  }

  getPage(page:number, pageSize:number, start?: Date | undefined, end?: Date | undefined, category?: string | undefined, status?: string | undefined, search?: string | undefined, assignedUser?: string | undefined, internetUser?: number | undefined) : Observable<ProblemPage> {
    let baseUrl = this.url.getApiUrl(Endpoints.problemPage(page,pageSize)) + this.generateArgs(start,end,category,status,search,assignedUser,internetUser);
    return this.http.get<ProblemPage>(baseUrl);
  }

  generateArgs(start?: Date | undefined, end?: Date | undefined, category?: string | undefined, status?: string | undefined, search?: string | undefined, assignedUser?: string | undefined, internetUser?: number | undefined) : string {
    let args ="?";
    if(typeof start === 'object') {
      args = "?" + "dateFrom=" + start.toLocaleDateString('en-GB');
    } 
      if(typeof end === 'object') {
        args += "&dateTo=" + end.toLocaleDateString('en-GN');
      }
      if(category !== undefined) {
        args += "&category=" + category;
      }  
      if(status !== undefined) {
        args += "&status=" + status;
      }  
      if(search !== undefined) {
        args += "&searchTerm=" + search;
      }      
      if(assignedUser !== undefined) {
        args += "&assingnedUser=" + assignedUser;
      }
      if(internetUser !== undefined) {
        args += "&internetUser=" + internetUser;
      }        
    return args;
  }

}

export interface ProblemPage {
    total : number;
    data : Problem[];
   }

export class CreateProblem {
  name: string;
  description: string;
  location: string;
  assignedUser: string| null;
  statusId: number;
  internetUserId: number| null;
  category: number;
  tags: string[];
}

export interface Problem {
  id: number;
  name: string;
  description: string;
  location: string;
  categoryName: string;
  assignedUserEmail: string;
  status: string;
  statusId: number;
  internetUserId: number;
  assignedUserId: string;
  created: Date;
}
