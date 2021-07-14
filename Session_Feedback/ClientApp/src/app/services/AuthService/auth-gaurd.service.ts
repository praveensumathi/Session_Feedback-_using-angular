import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NgForm } from "@angular/forms";
import { CanActivate, Router } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";
import { Subject } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class AuthGaurdService implements CanActivate {
  private user = new Subject();
  user$ = this.user.asObservable();

  constructor(
    private jwtHelper: JwtHelperService,
    private router: Router,
    private http: HttpClient
  ) {}

  canActivate() {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    this.router.navigate(["login"]);
    return false;
  }

  login(form: NgForm) {
    const credentials = JSON.stringify(form.value);
    this.http
      .post(`${location.origin}/api/account`, credentials, {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
        }),
        responseType: "text",
      })
      .subscribe(
        (response) => {
          this.user.next();
          const token = response.toString();
          localStorage.setItem("jwt", token);
          this.router.navigate(["home"]);
        },
        (err) => {
          this.user.error(null);
          console.log(err);
        }
      );
  }
}
