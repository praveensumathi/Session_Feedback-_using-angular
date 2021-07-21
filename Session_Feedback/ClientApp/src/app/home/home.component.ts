import { Component, OnInit, ViewChild } from "@angular/core";
import { ISession } from "../services/ApiService/SessionService/session";
import { SessionService } from "../services/ApiService/SessionService/session.service";
import { SessionDeleteModalComponent } from "./session-delete-modal/session-delete-modal.component";
import { SessionEditModelComponent } from "./session-edit-model/session-edit-model.component";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {
  public sessions: ISession[];

  constructor(private session: SessionService) {}

  @ViewChild("edit", { static: false })
  editModel: SessionEditModelComponent;

  @ViewChild("delete", { static: false })
  deleteModel: SessionDeleteModalComponent;

  ngOnInit() {
    this.GetAllSessions();
  }

  GetAllSessions() {
    debugger;
    this.session
      .GetAllSession()
      .subscribe((sessions) => (this.sessions = sessions));
  }

  openEditModal(session: ISession) {
    this.editModel.openVerticallyCentered(session);
  }

  openDeleteModal(session: ISession) {
    this.deleteModel.openVerticallyCentered(session);
  }
}
