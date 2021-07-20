import {
  AfterViewInit,
  Component,
  OnInit,
  TemplateRef,
  ViewChild,
} from "@angular/core";
import { ISession } from "../services/ApiService/SessionService/session";
import { SessionService } from "../services/ApiService/SessionService/session.service";
import { MatDialog } from "@angular/material/dialog";
import { SessionEditModelComponent } from "./session-edit-model/session-edit-model.component";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {
  public sessions: ISession[];

  constructor(private session: SessionService, public dialog: MatDialog) {}

  @ViewChild("edit", { static: false })
  editModel: SessionEditModelComponent;

  ngOnInit() {
    this.GetAllSessions();
  }

  GetAllSessions() {
    this.session
      .GetAllSession()
      .subscribe((sessions) => (this.sessions = sessions));
  }

  openEditModal(session: ISession) {
    this.editModel.openVerticallyCentered(session);
  }
}
