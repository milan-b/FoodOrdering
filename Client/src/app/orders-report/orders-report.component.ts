import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { MeniService } from '../_services/meni.service';
import { Meni } from '../_models/meni';
import { OrderService } from '../_services/order.service';
import { forkJoin } from 'rxjs';
import { Hrana } from '../_models/hrana';
@Component({
    selector: 'app-orders-report',
    templateUrl: './orders-report.component.html',
    styleUrls: ['./orders-report.component.less']
})
export class OrdersReportComponent implements OnInit {
    today: moment.Moment;
    menu: Meni;
    sideDishesMap = [];
    foodMap = [];
    allFood = [];
    orders = [];
    constructor(private meniService: MeniService, private orderService: OrderService) { }

    ngOnInit(): void {
        this.today = moment();
        forkJoin({
            food: this.meniService.getAllFood(),
            sideDishes: this.meniService.getAllSideDishes(),
            menu: this.meniService.getMenu(this.today),
        }).subscribe((data) => {
            this.menu = new Meni({ menuId: (<any>data.menu.body).menuId, date: (<any>data.menu.body).date, food: (<any>data.menu.body).food, canOrder: (<any>data.menu.body).canOrder });
            this.getOrders();

            this.allFood = data.food.body as any[];
            this.allFood.forEach(o => {
                this.foodMap[o.hranaId] = o.naziv;
            });

            (<any[]>data.sideDishes.body).forEach(o => {
                this.sideDishesMap[o.prilogId] = o.naziv;
            });
        });
    }

    onDateChange(event) {
        console.log('Datum: ' + event.value);
        this.meniService.getMenu(event.value)
            .subscribe((data: any) => {
                this.menu = new Meni({ menuId: data.body.menuId, date: data.body.date, food: data.body.food, canOrder: data.body.canOrder });
                this.getOrders();
            });
        //console.log(event.value);
    }

    getOrders() {
        if (this.menu.menuId != 0) {
            this.orderService.getAll(this.menu.menuId).subscribe(data => {
                console.log(data.body);
                this.orders = data.body as any[];
                for (let i = 0; i < this.orders.length; i++) {
                    console.log(this.foodMap[this.orders[i].foodId]);
                }
            })
        }
        else {
            this.orders = [];
        }
    }


}
