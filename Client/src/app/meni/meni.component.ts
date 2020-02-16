import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDatepickerIntl, MatDatepickerInputEvent } from '@angular/material/datepicker';
import { MeniService } from './meni.service';
import { Moment } from 'moment';
import * as moment from 'moment';
import { Meni } from '../_models/meni';

@Component({
  selector: 'app-meni',
  templateUrl: './meni.component.html',
  styleUrls: ['./meni.component.less']
})
export class MeniComponent implements OnInit {
  step = 0;

  constructor(private meniService: MeniService) { }
  meni: Meni
  hrana: any;
  date: FormControl;

  ngOnInit() {
    this.date = new FormControl(moment().toDate());
    this.getHrana(moment());
  }

  onDateChange(date) {
    this.getHrana(date.value);
  }

  setStep(index: number) {
    this.step = index;
  }

  nextStep() {
    this.step++;
  }

  prevStep() {
    this.step--;
  }

  getHrana(date: Moment) {
    this.meniService.getMenu(date).subscribe(response => {
      console.log(response);
      this.meni = new Meni(response);
      console.log(this.meni);
      //if (response && response.body) {
      //  this.hrana = (<any>response.body).hrana;
      //} else {
      //  this.hrana = null;
      //}
    });
  }



}
