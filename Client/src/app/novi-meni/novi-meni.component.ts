import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import * as moment from 'moment';
import { MeniService } from '../_services/meni.service';
import { HttpResponse } from '@angular/common/http';
import { Hrana } from '../_models/hrana';
import { Prilog } from '../_models/prilog';
import { MatDialog } from '@angular/material';
import { CreateFoodDialogComponent } from '../create-food-dialog/create-food-dialog.component';

@Component({
  selector: 'app-novi-meni',
  templateUrl: './novi-meni.component.html',
  styleUrls: ['./novi-meni.component.less']
})
export class NoviMeniComponent implements OnInit {
  date: FormControl;
  nextWeek: moment.Moment;
  hranaArray: Hrana[] = [];
  stalnaHranaArray: Hrana[] = [];
  filterHrana: string = "";
  filterStalnaHrana: string = "";
  step: number = -1;
  stepStalna: number = -1;

  constructor(private meniService: MeniService, private dialog: MatDialog) { }

  ngOnInit() {
    this.nextWeek = moment().add(1, 'week');
    //this.date = new FormControl(nextWeek.toDate());
    this.meniService.getAllFood().subscribe(this.setFood);

  }

  setFood = (data: HttpResponse<Object>) => {
    (<[]>data.body).forEach(dataForHrana => {
      let hrana = new Hrana(dataForHrana);
      if (hrana.stalna) {
        this.stalnaHranaArray.push(hrana);
      } else {
        this.hranaArray.push(hrana);
      }
    });
  }

  izaberiHranu(event, hrana: Hrana) {
    event.stopPropagation();
    hrana.izabrana = !hrana.izabrana;

  }

  kreirajHranu(): void {
    const dialogRef = this.dialog.open(CreateFoodDialogComponent, {
      width: '700px',
      height: '80%',
      disableClose: true
      //data: { name: this.name, animal: this.animal }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      //this.animal = result;
    });
  }

  onDateChange(date) {
    console.log('date changed: ', date);
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

  /// Steps region ///
  setStep(index: number) {
    this.step = index;
  }

  nextStep(event) {
    event.stopPropagation();
    this.step++;
  }

  prevStep(event) {
    event.stopPropagation();
    this.step--;
  }

  setStepStalna(index: number) {
    this.stepStalna = index;
  }

  nextStepStalna(event) {
    event.stopPropagation();
    this.stepStalna++;
  }

  prevStepStalna(event) {
    event.stopPropagation();
    this.stepStalna--;
  }
  /// end region //
}
