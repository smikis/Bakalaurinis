import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UrlService, Endpoints } from './url.service';
import { Observable } from 'rxjs/Observable';
import { LoginService } from './login.service';
import { RequestOptions, ResponseContentType, Headers } from '@angular/http';

@Injectable()
export class TagService {

    constructor(private http: HttpClient, private url: UrlService, private login: LoginService) {
    }

    getProblemTags(id: number): Observable<Tag[]> {
        let baseUrl = this.url.getApiUrl(Endpoints.getProblemTags(id));
        return this.http.get<Tag[]>(baseUrl);
    }

    createProblemTag(name: string, problemId: number): Observable<Tag> {       
        let baseUrl = this.url.getApiUrl(Endpoints.getProblemTags(problemId));
        var createTag = new Tag(name);
        return this.http.post<Tag>(baseUrl, createTag);
    }

    delete(timeSpentId: number) {
        let baseUrl = this.url.getApiUrl(Endpoints.getTag(timeSpentId));
        return this.http.delete(baseUrl);
    }

}

export class Tag {
    constructor(name: string) {
        this.name = name;
    }
    id: number;
    name: string;
}