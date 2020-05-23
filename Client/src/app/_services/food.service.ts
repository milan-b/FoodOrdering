import { Injectable } from '@angular/core';
import { DataService } from '../_services/data.service';
import { HttpParams, HttpClient } from '@angular/common/http';
import { Moment } from 'moment';
import { Meni } from '../_models/meni';
import { Hrana } from '../_models/hrana';
import { environment } from '../../environments/environment';
import { Prilog } from '../_models/prilog';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class FoodService {

    constructor(private http: HttpClient) { }

    getAllFood(): Observable<Hrana[]> {
        return this.http.get<Hrana[]>(`${environment.apiUrl}hrana/GetAll`);
    }

    getAllSideDishes(): Observable<Prilog[]> {
        return this.http.get<Prilog[]>(`${environment.apiUrl}hrana/GetAllSideDishes`);
    }

    createSideDish(data) {
        return this.http.post<any>(`${environment.apiUrl}hrana/CreateSideDish`, data);
    }

    createFood(data) {
        return this.http.post<any>(`${environment.apiUrl}hrana/CreateOrUpdate`, data);
    }

    rateFood(foodId: number, mark: number) {
        return this.http.post<any>(`${environment.apiUrl}hrana/Rate`, { mark: mark, foodId: foodId });
    }


}
