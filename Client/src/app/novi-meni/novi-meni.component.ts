import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import * as moment from 'moment';
import { MeniService } from '../_services/meni.service';
import { HttpResponse } from '@angular/common/http';
import { Hrana } from '../_models/hrana';
import { Prilog } from '../_models/prilog';
import { MatDialog } from '@angular/material';
import { CreateFoodDialogComponent } from '../create-food-dialog/create-food-dialog.component';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-novi-meni',
  templateUrl: './novi-meni.component.html',
  styleUrls: ['./novi-meni.component.less']
})
export class NoviMeniComponent implements OnInit {
  date: FormControl;
  nextWeek: moment.Moment;
  hranaArray: Hrana[];
  stalnaHranaArray: Hrana[];
  filterHrana: string = "";
  filterStalnaHrana: string = "";
  step: number = -1;
  stepStalna: number = -1;
  sideDishesMap: any[] = [];
  sideDishes: Prilog[];

  constructor(private meniService: MeniService, private dialog: MatDialog) { }

  ngOnInit() {
    this.nextWeek = moment().add(1, 'week');
    this.initFood();
  }

  initFood() {
    forkJoin({
      food: this.meniService.getAllFood(),
      sideDishes: this.meniService.getAllSideDishes()
    }).subscribe((data) => {
      this.setFood(data.food);
      //this.sideDishes = [];
      this.sideDishesMap = [];
      this.sideDishes = [...(<any[]>data.sideDishes.body).map(o => new Prilog({ prilogId: o.prilogId, naziv: o.naziv, varijanta: o.varijanta }))];
      (<any[]>data.sideDishes.body).forEach(o => {
        this.sideDishesMap[o.prilogId] = o.naziv;
      })
    });
  }

  setFood = (data: HttpResponse<Object>) => {
    this.stalnaHranaArray = [];
    this.hranaArray = [];
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

  kreirajHranu(permanent: boolean): void {
    const dialogRef = this.dialog.open(CreateFoodDialogComponent, {
      width: '700px',
      height: '80%',
      disableClose: true,
      data: {
        sideDishesMap: this.sideDishesMap,
        sideDishes: this.sideDishes,
        name: permanent ? this.filterStalnaHrana : this.filterHrana,
        permanent: permanent
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.initFood();
    });
  }

  editFood(food: Hrana): void {
    const dialogRef = this.dialog.open(CreateFoodDialogComponent, {
      width: '700px',
      height: '80%',
      disableClose: true,
      data: {
        food: food,
        sideDishesMap: this.sideDishesMap,
        sideDishes: this.sideDishes
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.initFood();
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
  /// end region ///
}
