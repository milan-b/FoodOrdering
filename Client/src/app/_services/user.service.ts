import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

import { environment } from '@environments/environment';
import { User } from '@app/_models';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UserService {
    //private noChacheHeader;
    constructor(private http: HttpClient) {
        //this.noChacheHeader = new HttpHeaders({
        //    'Cache-Control': 'no-cache',
        //    'Pragma': 'no-cache',
        //    'Expires': 'Sat, 01 Jan 2000 00:00:00 GMT'
        //})
    }

    getAll(): Observable<User[]> {
        return this.http.get<User[]>(`${environment.apiUrl}Users/GetAll`);
    }

    delete(userId: number): Observable<any> {
        let params = new HttpParams().set('id', userId.toString());
        return this.http.delete(`${environment.apiUrl}Users/Delete`, { params: params });
    }
}
