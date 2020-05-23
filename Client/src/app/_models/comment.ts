import { Hrana } from './hrana';
import { Prilog } from './prilog';
import { Data } from '@angular/router';

export class Comment {
    user: string;
    comment: string;
    date: Date;
    image: string;



    public constructor(init?: Partial<Comment>) {
        Object.assign(this, init);
    }


}
