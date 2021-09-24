import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VaccineOrder } from '../models/vaccineorder';
import { Vaccine } from '../models/vaccines';
import { AuthService } from './auth.service';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: "Bearer " + localStorage.getItem("token")
  })
};
@Injectable({
  providedIn: 'root'
})
export class VaccineService {
  vaccinesUrl = "https://localhost:5001/api/vaccines/";

  constructor(private http: HttpClient, private authService: AuthService) { }

  getVaccineNames(): Observable<Vaccine[]> {
    return this.http
    .get<Vaccine[]>(
      this.vaccinesUrl, 
      httpOptions
      )
  }
  
  orderVaccines(model: any) {
    model.userId = this.authService.decodedToken.nameid;
    return this.http
    .post(this.vaccinesUrl + "order", model, httpOptions);
  }

  getOrderHistory(): Observable<VaccineOrder[]> {
    return this.http
    .get<VaccineOrder[]>(
      this.vaccinesUrl + "history", 
      httpOptions
      )
  }
}
