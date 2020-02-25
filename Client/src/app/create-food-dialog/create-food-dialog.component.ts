import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { MeniService } from '../meni/meni.service';
import { Prilog } from '../_models/prilog';
import { BarService } from '../_services/bar.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-create-food-dialog',
  templateUrl: './create-food-dialog.component.html',
  styleUrls: ['./create-food-dialog.component.less']
})
export class CreateFoodDialogComponent implements OnInit {
  createFoodForm: FormGroup;
  submitted: boolean = false;
  sideDishes: Array<Prilog> = [];

  constructor(private formBuilder: FormBuilder, private menueService: MeniService, private barService: BarService,
    public dialogRef: MatDialogRef<CreateFoodDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    //this.createFoodForm = this.formBuilder.group({
    //  name: ['', Validators.required],
    //  permanent: ['false'],
    //  sideDishes: 
    //});

    this.menueService.getAllSideDishes().subscribe(data => {
      this.sideDishes = (<[]>data.body).map((o: any) => new Prilog({ naziv: o.naziv, prilogId: o.prilogId }));
      const sideDishesFC = this.formBuilder.array(this.sideDishes.map(() => this.getVariantsFormControl()));
      this.createFoodForm = this.formBuilder.group({
        name: ['', Validators.required],
        permanent: ['false'],
        sideDishes: sideDishesFC
      });
    });
  }

  getVariantsFormControl(): FormGroup {
    const variantsFG = this.formBuilder.group({
      all: [false],
      first: [false],
      second: [false]
    });
    return variantsFG;
  }

  get f() {
    return this.createFoodForm.controls;
  }

  renderHelper = () => { };

  createSideDish(newSideDishInput) {
    if (newSideDishInput.value === '') {
      this.barService.showError('Morate unijeti naziv priloga.');
    }
    else {
      this.menueService.createSideDish({ naziv: newSideDishInput.value }).subscribe((sideDishId: number) => {
        this.sideDishes.push(new Prilog({ naziv: newSideDishInput.value, prilogId: sideDishId }));
        this.barService.showInfo("Prilog je uspješno kreiran.");
        newSideDishInput.value = '';
      })
    }

  }

  closeDialog = () => {
    this.dialogRef.close();
  }

  onSubmit() {
    this.submitted = true;

    console.log(this.createFoodForm);

    // stop here if form is invalid
    //if (!this.createFoodForm.invalid) {

    //  //this.loading = true;
    //  this.authenticationService.login(this.f.username.value, this.f.password.value)
    //    .pipe(first())
    //    .subscribe(
    //      data => {
    //        this.router.navigate([this.returnUrl]);
    //      },
    //      error => {
    //        this.error = error;
    //        this.loading = false;
    //      });
    //}
  }

}
