import { Component, OnInit } from "@angular/core";
import { ISession } from "../services/ApiService/SessionService/session";
import { SessionService } from "../services/ApiService/SessionService/session.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {
  public sessions: ISession[];

  constructor(private session: SessionService) {}

  ngOnInit() {
    this.session
      .GetAllSession()
      .subscribe((sessions) => (this.sessions = sessions));
  }
}
