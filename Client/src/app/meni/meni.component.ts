import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDatepickerIntl, MatDatepickerInputEvent, MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { MeniService } from './meni.service';
import { Moment } from 'moment';
import * as moment from 'moment';
import { Meni } from '../_models/meni';
import { Hrana } from '../_models/hrana';
import { Prilog } from '../_models/prilog';
import { MeniForCalendar } from '../_models/meniForCalendar';

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
  datesWithMenue: Array<MeniForCalendar>;

  ngOnInit() {
    let nextDay = moment().add(1, 'days');
    this.getHrana(nextDay);
    this.date = new FormControl(nextDay.toDate());

    this.setDatesWithMenus();
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
    });
  }

  setDatesWithMenus() {
    this.meniService.getAllMenus().subscribe(response => {
      this.datesWithMenue = (<any[]>response.body).map(item => { return { meniId: item.meniId, datum: new Date(item.datum) } });
    });
  }

  dateWithMenuClass = (d: Moment): MatCalendarCellCssClasses => {
    const date = d.toDate().getTime();
    return !this.datesWithMenue.every(menue => menue.datum.getTime() != date) ? 'date-with-menue' : '';
  }

  onPrilogChange(hrana: Hrana, prilog: Prilog) {
    if (prilog.varijanta != 0) {
      if (prilog.izabran) {
        let izabranaVarijanta = prilog.varijanta;
        hrana.prilozi.forEach(prilogItem => {
          if (prilogItem.varijanta != 0 && prilogItem.varijanta != izabranaVarijanta) {
            prilogItem.omogucen = false;
          }
        });
      } else {
        if (hrana.prilozi.every(prilogItem => { return !prilogItem.izabran || prilogItem.varijanta == 0 })) {
          hrana.prilozi.forEach(prilogItem => {
            prilogItem.omogucen = true;
          });
        }
      }
    }
  }



}
