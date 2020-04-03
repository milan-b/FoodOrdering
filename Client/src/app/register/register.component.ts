import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../_services';
import { Router, ActivatedRoute } from '@angular/router';
import { ROLES } from '../globas';

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

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private authenticationService: AuthenticationService
    ) {

    }

    ngOnInit() {
        this.roles = Object.keys(ROLES).map(key => ROLES[key]);
        this.registerForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            role: [ROLES.member, Validators.required]
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

    ngOnDestroy() {
        //this.qpSupcription.unsubscribe();
    }
}
