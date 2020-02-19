import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import * as moment from 'moment';

@Component({
  selector: 'app-novi-meni',
  templateUrl: './novi-meni.component.html',
  styleUrls: ['./novi-meni.component.less']
})
export class NoviMeniComponent implements OnInit {
  date: FormControl;
  constructor() { }

  ngOnInit() {
    let nextDay = moment().add(1, 'days');
    this.date = new FormControl(nextDay.toDate());
  }


}
