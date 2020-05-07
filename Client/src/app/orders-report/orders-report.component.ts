import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { MeniService } from '../_services/meni.service';
import { Meni } from '../_models/meni';
import { OrderService } from '../_services/order.service';
@Component({
    selector: 'app-orders-report',
    templateUrl: './orders-report.component.html',
    styleUrls: ['./orders-report.component.less']
})
export class OrdersReportComponent implements OnInit {
    today: moment.Moment;
    menu: Meni;
    constructor(private meniService: MeniService, private orderService: OrderService) { }

    ngOnInit(): void {
        this.today = moment();
    }
    onDateChange(event) {
        this.meniService.getMenu(event.value)
            .subscribe((data: any) => {
                this.menu = new Meni({ menuId: data.body.menuId, date: data.body.date, food: data.body.food, canOrder: data.body.canOrder });
                this.orderService.getAll(this.menu.menuId).subscribe(data => console.log(data.body))
            });
        //console.log(event.value);
    }
}
