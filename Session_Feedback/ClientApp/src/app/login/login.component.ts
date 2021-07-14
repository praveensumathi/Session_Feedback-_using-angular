import { Component, OnInit } from "@angular/core";
import { FormControl, NgForm } from "@angular/forms";
import { JwtHelperService } from "@auth0/angular-jwt";
import { AuthGaurdService } from "../services/AuthService/auth-gaurd.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  name: string = "";
  password: string = "";
  invalidLogin: boolean = this.authService.invalidLogin;

  constructor(private authService: AuthGaurdService) {}

  ngOnInit() {}

  login(form: NgForm) {
    this.authService.login(form);
  }
}
