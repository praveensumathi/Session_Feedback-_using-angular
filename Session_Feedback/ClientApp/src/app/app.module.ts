import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { JwtModule } from "@auth0/angular-jwt";
import { MatMenuModule } from "@angular/material/menu";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { NgbModule, NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { LoginComponent } from "./login/login.component";
import { HomeComponent } from "./home/home.component";
import { AuthGaurdService } from "./services/AuthService/auth-gaurd.service";
import { SessionDetailsComponent } from "./session-details/session-details.component";
import { SessionEditModelComponent } from "./home/session-edit-model/session-edit-model.component";
import { SessionDeleteModalComponent } from "./home/session-delete-modal/session-delete-modal.component";
import { SessionAddModalComponent } from "./home/session-add-modal/session-add-modal.component";

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    HomeComponent,
    SessionDetailsComponent,
    SessionEditModelComponent,
    SessionDeleteModalComponent,
    SessionAddModalComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    RouterModule.forRoot([
      {
        path: "home",
        component: HomeComponent,
        canActivate: [AuthGaurdService],
        pathMatch: "full",
      },
      { path: "login", component: LoginComponent },
      {
        path: "detail/:id",
        component: SessionDetailsComponent,
        canActivate: [AuthGaurdService],
      },
      {
        path: "",
        component: LoginComponent,
        pathMatch: "full",
      },
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: [
          "https://localhost:5001/",
          "https://localhost:44308/",
        ],
        blacklistedRoutes: [],
        throwNoTokenError: true,
        skipWhenExpired: true,
      },
    }),
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    BrowserAnimationsModule,
  ],
  providers: [AuthGaurdService],
  bootstrap: [AppComponent],
  entryComponents: [],
})
export class AppModule {}
