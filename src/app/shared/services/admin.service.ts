import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AuthService } from "./auth.service";
import { catchError, map, tap } from "rxjs/operators";
import { Vaccine } from "../models/vaccines";
import { Observable } from "rxjs";

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: "Bearer " + localStorage.getItem("token")
  })
};
@Injectable({
  providedIn: "root"
})
export class AdminService {
  adminsUrl = "https://localhost:5001/api/admins/";

  constructor(private http: HttpClient, private authService: AuthService) {}

  updateAdmin(model: any) {
    const formData = new FormData();
    formData.append("profileimage", model.file, model.file.name);
    return this.http
      .put(
        this.adminsUrl + "update/" + this.authService.decodedToken.nameid,
        formData,
        httpOptions
      )
      .pipe(
        map((response: any) => {
          const user = response;
          if (user.result.succeeded) {
            this.authService.currentUser = user.AdminToReturn;
          }
        })
      );
  }
}