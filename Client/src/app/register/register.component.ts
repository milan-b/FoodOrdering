import { Component, OnInit, ViewChild } from '@angular/core';
import { first } from 'rxjs/operators';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AuthenticationService, UserService } from '../_services';
import { Router, ActivatedRoute } from '@angular/router';
import { ROLES } from '../globas';
import { User } from '../_models';
import { MatSort, MatTableDataSource, MatDialog } from '@angular/material';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.less']
})
export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    roles: string[];
    error = '';
    qpSupcription: Subscription;
    users: Array<User>;
    userFilter: string = "";


    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private authenticationService: AuthenticationService,
        private userService: UserService,
        private dialog: MatDialog
    ) {

    }

    ngOnInit() {
        this.roles = Object.keys(ROLES).map(key => ROLES[key]);
        this.registerForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            role: [ROLES.member, Validators.required]
        });
        this.loading = true;
        this.userService.getAll().pipe(first()).subscribe(users => {
            this.loading = false;
            this.users = users;
            console.log(users);
        });


    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }

    onSubmit() {
        console.log(this.registerForm.controls.email.errors);
        this.submitted = true;

        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }

        this.loading = true;
        this.authenticationService.login(this.f.username.value, this.f.password.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.router.navigate([this.returnUrl]);
                },
                error => {
                    this.error = error;
                    this.loading = false;
                });
    }

    resetPassword(user: User) {
        console.log('not implemented\n', user);
    }

    delete(user: User) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            width: '700px',
            height: '80%',
            disableClose: true,
            data: {
                message: `Da li ste sigurni da želite obrisati korisnika "${user.username}"?`
            }
        });

        dialogRef.afterClosed().subscribe(result => {
            console.log(result);
        });
        console.log('not implemented\n', user);
    }

    ngOnDestroy() {
        //this.qpSupcription.unsubscribe();
    }
}
