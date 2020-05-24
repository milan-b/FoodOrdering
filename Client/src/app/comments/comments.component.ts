import { Component, OnInit, Input } from '@angular/core';
import { Hrana } from '../_models/hrana';
import { FoodService } from '../_services/food.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BarService } from '../_services/bar.service';
import { Comment } from '../_models/comment';
import * as moment from 'moment';

@Component({
    selector: 'app-comments',
    templateUrl: './comments.component.html',
    styleUrls: ['./comments.component.less']
})
export class CommentsComponent implements OnInit {
    @Input() food: Hrana;

    commentForm: FormGroup;
    comments: Comment[];

    loading: boolean  = false;

    constructor(private foodService: FoodService, private formBuilder: FormBuilder, private barService: BarService) { }

    ngOnInit(): void {
        this.commentForm = this.formBuilder.group({
            comment: ["", Validators.required]
        })
    }

    onSubmit() {
        if (!this.commentForm.valid) {
            this.barService.showError("Komentar ne može da bude prazan!");
        } else {
            this.foodService.setComment(this.food.hranaId, this.commentForm.value.comment).subscribe(
                () => {
                    this.barService.showInfo("Komentar je snimljen.");
                    this.getComments();
                    this.commentForm.reset();
                },
                error => this.barService.showError("Dogodila se greška. Molimo Vas pokušajte kasnije.\nDetalji:" + error)
            );
        }
    }

    onOpen() {
        if (!this.comments) {
            this.getComments();
        }
    }

    getComments() {
        this.loading = true;
        this.foodService.getComments(this.food.hranaId).subscribe(o => {
            this.comments = o;
            this.loading = false;
        });
    }


    public formatTime(time: Date) {
        return moment(time).format('DD.MM.YYYY  HH:mm:ss');
    }


}
