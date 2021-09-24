import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { map } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";
import { User } from "../models/user";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  authUrl = "https://localhost:5001/api/auth/";
  adminsUrl = "https://localhost:5001/api/admins/";
  confirmEmailUrl = "http://localhost:4200/confirm-email/";
  changePasswordUrl = "http://localhost:4200/change-password/";
  helper = new JwtHelperService();
  decodedToken: any;
  currentUser: User | null | undefined; // null | undefined

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post(this.authUrl + "login", model).pipe(
      map((response: any) => {
        const user = response;
        if (user.result.succeeded) {
          localStorage.setItem("token", user.token);
          localStorage.setItem("user", JSON.stringify(user.userToReturn));
          this.decodedToken = this.helper.decodeToken(user.token);
          this.currentUser = user.userToReturn;
        }
      })
    );
  }

  register(model: any) {
    let headers = new HttpHeaders({
      confirmEmailUrl: this.confirmEmailUrl
    });
    let options = { headers: headers };
    return this.http.post(this.adminsUrl + "create", model, options);
  }

  resetPassword(model: any) {
    let headers = new HttpHeaders({
      changePasswordUrl: this.changePasswordUrl
    });
    let options = { headers: headers };
    return this.http.post(this.authUrl + "resetpassword", model, options);
  }

  confirmEmail(model: any) {
    return this.http.post(this.authUrl + "confirmemail", model);
  }

  changePassword(model: any) {
    return this.http.post(this.authUrl + "changepassword", model);
  }

  loggedIn() {
    const token = localStorage.getItem("token");
    //console.log(`token: ${token}`);
    return !this.helper.isTokenExpired(token!);
  }
}