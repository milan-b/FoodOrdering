import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ErrorBarComponent } from '../error-bar/error-bar.component';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {
  durationInSeconds = 10;

  constructor(private snackBar: MatSnackBar) { }

  showError(errorMessage: string) {
    this.snackBar.openFromComponent(ErrorBarComponent, {
      duration: this.durationInSeconds * 1000,
      data: errorMessage
    });
  }
}
