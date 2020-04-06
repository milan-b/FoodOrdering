import { MatSort } from '@angular/material';

export class User {
    id: number;
    username: string;
    password: string;
    firstName: string;
    lastName: string;
    token?: string;
    roles?: string;

}
