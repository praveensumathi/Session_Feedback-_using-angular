import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { JwtModule } from "@auth0/angular-jwt";
import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { LoginComponent } from "./login/login.component";
import { HomeComponent } from "./home/home.component";
import { AuthGaurdService } from "./services/AuthService/auth-gaurd.service";
// import { FetchDataComponent } from './fetch-data/fetch-data.component';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    HomeComponent,
    // FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {
        path: "",
        component: LoginComponent,
        pathMatch: "full",
      },
      {
        path: "home",
        component: HomeComponent,
        canActivate: [AuthGaurdService],
        pathMatch: "full",
      },
      { path: "login", component: LoginComponent },
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:5001", "https://localhost:44308/"],
        blacklistedRoutes: [],
        throwNoTokenError: true,
        skipWhenExpired: true,
      },
    }),
  ],
  providers: [AuthGaurdService],
  bootstrap: [AppComponent],
})
export class AppModule {}
