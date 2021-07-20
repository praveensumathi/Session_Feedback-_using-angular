import { AfterViewInit, Component, ViewChild } from "@angular/core";
import { NgbDateStruct, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import * as _ from "lodash";
import { ISession } from "src/app/services/ApiService/SessionService/session";
import { SessionService } from "src/app/services/ApiService/SessionService/session.service";

@Component({
  selector: "app-session-edit-model",
  templateUrl: "./session-edit-model.component.html",
  styleUrls: ["./session-edit-model.component.css"],
})
export class SessionEditModelComponent implements AfterViewInit {
  model: NgbDateStruct;
  placement = "bottom";

  private editModelContent: any;
  selectedSession: ISession;

  sessionName: string;
  conductedBy: string;
  conductedOn: Date;
  isUpdated: boolean = false;
  error: string = null;

  constructor(
    private modalService: NgbModal,
    private sessionService: SessionService
  ) {}

  @ViewChild("content", { static: false })
  editModel: any;

  ngAfterViewInit() {
    this.editModelContent = this.editModel;
  }

  dismissModal() {
    this.modalService.dismissAll();
  }
  closeModal() {
    this.modalService.dismissAll();
  }
  openVerticallyCentered(session: ISession) {
    debugger;
    this.selectedSession = session;
    this.sessionName = session.name;
    this.conductedBy = session.conductedBy;
    this.conductedOn = session.conductedOn;
    this.modalService.open(this.editModelContent, { centered: true });
  }

  onConductedByChange(e) {
    if (_.isEmpty(e)) {
      this.conductedBy = null;
    }
  }
  onNameChange(e) {
    if (_.isEmpty(e)) {
      this.sessionName = null;
    }
  }

  updatedSession() {
    var updateSession: ISession = {
      ...this.selectedSession,
      name: this.sessionName,
      conductedBy: this.conductedBy,
      conductedOn: this.conductedOn,
    };

    this.sessionService.UpdateSession(updateSession).subscribe(
      (isUpdated) => {
        if (isUpdated) {
          this.isUpdated = isUpdated;
        }
      },
      (error) => {
        this.error = error;
        this.isUpdated = false;
      }
    );
  }
}