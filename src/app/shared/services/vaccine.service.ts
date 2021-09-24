import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VaccineOrder } from '../models/vaccineorder';
import { Vaccine } from '../models/vaccines';
import { AuthService } from './auth.service';


@Injectable({
  providedIn: 'root'
})
export class VaccineService {
  vaccinesUrl = "https://localhost:5001/api/vaccines/";

  constructor(private http: HttpClient, private authService: AuthService) { }

  getVaccineNames(): Observable<Vaccine[]> {
    return this.http
    .get<Vaccine[]>(
      this.vaccinesUrl
      )
  }
  
  orderVaccines(model: any) {
    model.userId = this.authService.decodedToken.nameid;
    return this.http
    .post(this.vaccinesUrl + "order", model);
  }

  getOrderHistory(): Observable<VaccineOrder[]> {
    return this.http
    .get<VaccineOrder[]>(
      this.vaccinesUrl + "history"
      )
  }
}
