import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService, Endpoints } from './url.service';
import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { RequestOptions,ResponseContentType, Headers } from '@angular/http';

@Injectable()
export class CommentService {

  constructor(private http: HttpClient, private url: UrlService, private login: LoginService) {
  }

  createComment(text:string, userId: string, problemId: number) {
    var comment = new CreateComment();
    comment.text = text;
    comment.userId = userId;
    comment.problemId = problemId;

    let baseUrl = this.url.getApiUrl(Endpoints.comment);
    return this.http.post(baseUrl, comment, {headers: this.login.applyAuthentication()});
  }

  getProblemComments(problemId: number) {
    let baseUrl = this.url.getApiUrl(Endpoints.getProblemComments(problemId));
    return this.http.get<Array<Comment>>(baseUrl, {headers: this.login.applyAuthentication()});
  }

}

export class CreateComment {
  text: string;
  userId: string;
  problemId: number; 
}

export class Comment {
  id: number;
  text: string;
  firstName: string;
  lastName: string;
}
