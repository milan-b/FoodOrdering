import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDatepickerIntl } from '@angular/material/datepicker';
import { MeniService } from './meni.service';

@Component({
  selector: 'app-meni',
  templateUrl: './meni.component.html',
  styleUrls: ['./meni.component.less']
})
export class MeniComponent implements OnInit {
  step = 0;

  constructor(private meniService: MeniService) { }
  hrana: any;

  ngOnInit() {
    this.meniService.getMenu().subscribe(response => {
      if (response) {
        this.hrana = (<any>response.body).hrana;
      }
    });
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

  date = new FormControl(new Date());
  serializedDate = new FormControl((new Date()).toISOString());




}
