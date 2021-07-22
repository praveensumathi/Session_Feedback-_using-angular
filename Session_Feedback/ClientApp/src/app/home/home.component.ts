import { Component, OnInit, ViewChild } from "@angular/core";
import { ISession } from "../services/ApiService/SessionService/session";
import { SessionService } from "../services/ApiService/SessionService/session.service";
import { SessionAddModalComponent } from "./session-add-modal/session-add-modal.component";
import { SessionDeleteModalComponent } from "./session-delete-modal/session-delete-modal.component";
import { SessionEditModelComponent } from "./session-edit-model/session-edit-model.component";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  public sessions: ISession[];

  constructor(private session: SessionService) {}

  @ViewChild("add", { static: false })
  addModal: SessionAddModalComponent;

  @ViewChild("edit", { static: false })
  editModel: SessionEditModelComponent;

  @ViewChild("delete", { static: false })
  deleteModel: SessionDeleteModalComponent;

  ngOnInit() {
    this.GetAllSessions();
  }

  GetAllSessions() {
    this.session.GetAllSession().subscribe(
      (sessions) =>
        (this.sessions = sessions.map((session) => {
          return {
            ...session,
            conductedOn: session.conductedOn
              ? new Date(session.conductedOn)
              : null,
          };
        }))
    );
  }

  openAddModal() {
    this.addModal.openVerticallyCentered();
  }
  openEditModal(session: ISession) {
    this.editModel.openVerticallyCentered(session);
  }

  openDeleteModal(session: ISession) {
    this.deleteModel.openVerticallyCentered(session);
  }
}
