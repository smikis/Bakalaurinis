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


  getPage(page:number, pageSize:number, start?: Date | undefined, end?: Date | undefined, category?: string | undefined, status?: string | undefined, search?: string | undefined) : Observable<Problem[]> {
    let baseUrl = this.url.getApiUrl(Endpoints.problemPage(page,pageSize)) + this.generateArgs(start,end,category,status,search);
    return this.http.get<Problem[]>(baseUrl);
  }

  generateArgs(start?: Date | undefined, end?: Date | undefined, category?: string | undefined, status?: string | undefined, search?: string | undefined) : string {
    let args ="?";
    console.log(start);
    console.log(typeof(start));
    if(start !== undefined && start!== null) {
      args = "?" + "dateFrom=" + start;
    }   
      if(end !== undefined) {
        args += "&dateTo=" + end;
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
    return args;
  }

}


export interface Problem {
  id: number;
  name: string;
  description: string;
  location: string;
  categoryName: string;
  assignedUserEmail: string;
  status: string;
}
