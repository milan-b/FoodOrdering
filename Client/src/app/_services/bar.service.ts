import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarVerticalPosition, MatSnackBarConfig } from '@angular/material';
import { ErrorBarComponent } from '../error-bar/error-bar.component';
import { InfoBarComponent } from '../info-bar/info-bar.component';

@Injectable({
  providedIn: 'root'
})
export class BarService {
  durationInSeconds = 10;

  constructor(private snackBar: MatSnackBar) {}

  showError(errorMessage: string) {
    this.snackBar.openFromComponent(ErrorBarComponent, {
      duration: this.durationInSeconds * 1000,
      data: errorMessage,
      verticalPosition: 'top' 
    });
  }

  showInfo(infoMessage: string) {
    this.snackBar.openFromComponent(InfoBarComponent, {
      duration: this.durationInSeconds * 1000,
      data: infoMessage,
      verticalPosition: 'top'
    });
  }
}
