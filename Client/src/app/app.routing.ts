import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { AuthGuard } from './_helpers';

import { BookcaseComponent } from './bookcase/bookcase.component';
import { MeniComponent } from './meni/meni.component';
import { NoviMeniComponent } from './novi-meni/novi-meni.component';
import { AdminGuard } from './_helpers/admin.guard';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'meni', component: MeniComponent, canActivate: [AuthGuard] },
    { path: 'novi-meni', component: NoviMeniComponent, canActivate: [AuthGuard] },
    { path: 'user-managment', component: RegisterComponent, canActivate: [AdminGuard] },
    //{ path: 'bookcase', component: BookcaseComponent },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);

