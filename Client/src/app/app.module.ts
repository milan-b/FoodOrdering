import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';

// used to create fake backend
import { fakeBackendProvider } from './_helpers';

import { AppComponent } from './app.component';
import { appRoutingModule } from './app.routing';

import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MatCardModule,
  MatFormFieldModule,
  MatGridListModule,
  MatRadioModule,
  MatDatepickerModule,
  MatExpansionModule,
  MatIconModule,
  MAT_DATE_LOCALE,
  MatCheckboxModule

} from '@angular/material';
import { MatMomentDateModule } from '@angular/material-moment-adapter';

import { BookcaseComponent } from './bookcase/bookcase.component';
import { MeniComponent } from './meni/meni.component';
import { NoviMeniComponent } from './novi-meni/novi-meni.component';


@NgModule({
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    appRoutingModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatGridListModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatRadioModule,
    MatMomentDateModule,
    MatExpansionModule,
    MatIconModule,
    MatCheckboxModule,
    FlexLayoutModule
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    BookcaseComponent,
    MeniComponent,
    NoviMeniComponent

  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: MAT_DATE_LOCALE, useValue: 'sr' },

    // provider used to create fake backend
    //fakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
