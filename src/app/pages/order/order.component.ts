import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AlertService } from '@full-fledged/alerts';
import { Vaccine } from 'src/app/shared/models/vaccines';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';
import { VaccineService } from 'src/app/shared/services/vaccine.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
  
  vaccines: Vaccine[] = [];
  vaccineSelected?: Vaccine;

  constructor(private vaccineService: VaccineService, private alertService: AlertService, private progressBar: ProgressBarService) { }

  ngOnInit(): void {
    this.getVaccineNames();
  }

  getVaccineNames(): void {
    this.vaccineService.getVaccineNames()
    .subscribe(vaccines => this.vaccines = vaccines);
  }

  onSubmit(f: NgForm) {
    console.log(f.value);  // { vaccineType: '', quantity: '' }

    this.alertService.info('Processing Order');
    this.progressBar.startLoading();
    const orderObserver = {
      next: (x: any) => {
        console.log('Order Complete');
        this.progressBar.setSuccess();
        this.alertService.success('Order Complete');
        f.resetForm();
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        console.log('Order error:');
        console.log(err);
        this.progressBar.setError();
        this.alertService.danger('Order Failed');
        this.progressBar.completeLoading();
      }
    };
    this.vaccineService.orderVaccines(f.value).subscribe(orderObserver);

  }

}
