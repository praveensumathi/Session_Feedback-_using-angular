import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";
import { AuthGaurdService } from "../services/AuthService/auth-gaurd.service";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"],
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isAuthenticated = false;

  constructor(
    private jwtHelper: JwtHelperService,
    private authService: AuthGaurdService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authService.user$.subscribe({
      next: () => (this.isAuthenticated = true),
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logOut() {
    localStorage.removeItem("jwt");
    this.router.navigate(["login"]);
    this.isAuthenticated = false;
  }
}
