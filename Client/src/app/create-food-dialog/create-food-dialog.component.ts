import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { MeniService } from '../_services/meni.service';
import { Prilog } from '../_models/prilog';
import { BarService } from '../_services/bar.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Hrana } from '../_models/hrana';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-create-food-dialog',
  templateUrl: './create-food-dialog.component.html',
  styleUrls: ['./create-food-dialog.component.less']
})
export class CreateFoodDialogComponent implements OnInit {
  createFoodForm: FormGroup;
  submitted: boolean = false;
  sideDishes: Array<Prilog> = [];
  mapVariantToVariangId = [];


  constructor(private formBuilder: FormBuilder, private menueService: MeniService, private barService: BarService,
    public dialogRef: MatDialogRef<CreateFoodDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    //this.mapVariantToVariangId['all'] = 0;
    //this.mapVariantToVariangId['first'] = 1;
    //this.mapVariantToVariangId['second'] = 2;

  }

  ngOnInit(): void {
    //this.createFoodForm = this.formBuilder.group({
    //  name: ['', Validators.required],
    //  permanent: ['false'],
    //  sideDishes: 
    //});

    this.menueService.getAllSideDishes().subscribe(data => {
      this.sideDishes = (<[]>data.body).map((o: any) => new Prilog({ naziv: o.naziv, prilogId: o.prilogId }));
      const sideDishesFC = this.formBuilder.array(this.sideDishes.map((o) => this.getVariantsFormControl(o.prilogId)));
      this.createFoodForm = this.formBuilder.group({
        name: ['', Validators.required],
        permanent: [false],
        sideDishes: sideDishesFC
      });
    });
  }

  getVariantsFormControl(prilogId: number): FormGroup {
    const variantsFG = this.formBuilder.group({
      all: [false],
      first: [false],
      second: [false],
      prilogId: [prilogId]
    });
    return variantsFG;
  }

  onVariantChage(i: any, formControlName: string) {
    const variantsFG = (<any>this.createFoodForm.get('sideDishes')).controls[i];
    const formControlValue = variantsFG.controls[formControlName].value;
    switch (formControlName) {
      case 'all': {
        if (formControlValue) {
          variantsFG.controls['first'].disable();
          variantsFG.controls['second'].disable();
        }
        else {
          variantsFG.controls['first'].enable();
          variantsFG.controls['second'].enable();
        }
        break;
      }
      case 'first': {
        if (formControlValue) {
          variantsFG.controls['second'].disable();
          variantsFG.controls['all'].disable();
        }
        else {
          variantsFG.controls['second'].enable();
          variantsFG.controls['all'].enable();
        }
        break;
      }
      case 'second': {
        if (formControlValue) {
          variantsFG.controls['first'].disable();
          variantsFG.controls['all'].disable();
        }
        else {
          variantsFG.controls['first'].enable();
          variantsFG.controls['all'].enable();
        }
        break;
      }
    }
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
    //TODO validete side dishes - if all selected, variant can not be. 
    this.submitted = true;


    if (!this.createFoodForm.invalid) {
      console.log(this.createFoodForm.value);
      const formValue = this.createFoodForm.value;
      const newFood = {
        Naziv: formValue.name,
        stalna: formValue.permanent,
        prilozi: this.getPrilozi(formValue.sideDishes)
      };

      //newFood.prilozi = this.getPrilozi(formValue.sideDishes);

      console.log(newFood);
      //this.loading = true;
      this.menueService.createFood(newFood).subscribe(() => {
        this.barService.showInfo("Hrana je uspješno kreirana.");
        this.dialogRef.close();
      }, (error) => {
          this.barService.showError("Dogidila se greška.");
      });

      //this.authenticationService.login(this.f.username.value, this.f.password.value)
      //  .pipe(first())
      //  .subscribe(
      //    data => {
      //      this.router.navigate([this.returnUrl]);
      //    },
      //    error => {
      //      this.error = error;
      //      this.loading = false;
      //    });
    }
  }

  getPrilozi(variant: any[]): any[] {
    let ret = [];
    variant.forEach(item => {
      if (item.all) {
        ret.push({ varijanta: 0, prilog: { prilogId: item.prilogId } });
      }
      else if (item.first) {
        ret.push({ varijanta: 1, prilog: { prilogId: item.prilogId }});
      }
      else if (item.second) {
        ret.push({ varijanta: 2, prilog: { prilogId: item.prilogId } });
      }
    });

    return ret;
  }


}
