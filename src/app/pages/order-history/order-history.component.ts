import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { AlertService } from '@full-fledged/alerts';
import { VaccineOrder } from 'src/app/shared/models/vaccineorder';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';
import { VaccineService } from 'src/app/shared/services/vaccine.service';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.scss']
})
export class OrderHistoryComponent implements OnInit {
  orderHistory: VaccineOrder[] = [];
  displayedColumns: string[] = ['id', 'name', 'quantity', 'user', 'date'];

  constructor(private vaccineService: VaccineService, private alertService: AlertService, private progressBar: ProgressBarService) { }

  ngOnInit(): void {
    this.getOrderHistory();
  }

  getOrderHistory(): void {
    this.vaccineService.getOrderHistory()
    .subscribe(orderHistory => { 
      this.orderHistory = orderHistory
      // ,console.log(this.orderHistory)
    });
  }

}
