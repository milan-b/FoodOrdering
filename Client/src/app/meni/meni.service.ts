import { Injectable } from '@angular/core';
import { DataService } from '../_services/data.service';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MeniService {

  constructor(private dataService: DataService) { }

  getMenu() {
    let params = new HttpParams().set('date', '2020-02-13');
    return this.dataService.get('Meni/GetMeni', params);
  }
}
