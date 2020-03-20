import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatCalendarCellCssClasses } from '@angular/material';
import { Moment } from 'moment';
import * as moment from 'moment';
import { MeniForCalendar } from '../_models/meniForCalendar';
import { MeniService } from '../_services/meni.service';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.less']
})
export class DatePickerComponent implements OnInit {
  date: FormControl;
  datesWithMenue: Array<MeniForCalendar>;
  @Input() initDate: Moment;
  @Output() dateChange: EventEmitter<any> = new EventEmitter();

  constructor(private meniService: MeniService) { }

  ngOnInit(): void {
    this.date = new FormControl(this.initDate.toDate());
    this.setDatesWithMenus();
  }

  onDateChange(date) {
    this.dateChange.emit(date);
  }

  setDatesWithMenus = () => {
    this.meniService.getAllMenus().subscribe(response => {
      this.datesWithMenue = (<any[]>response.body).map(item => { return { meniId: item.meniId, datum: new Date(item.datum) } });
    });
  }

  dateWithMenuClass = (d: Moment): MatCalendarCellCssClasses => {
    const date = d.toDate().getTime();
    return this.datesWithMenue && !this.datesWithMenue.every(menue => menue.datum.getTime() != date) ? 'date-with-menue' : '';
  }

}
