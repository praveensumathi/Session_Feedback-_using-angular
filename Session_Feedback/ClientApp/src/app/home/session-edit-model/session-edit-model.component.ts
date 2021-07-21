import { Component, EventEmitter, Output, ViewChild } from "@angular/core";
import { NgbDate, NgbModal, NgbModalOptions } from "@ng-bootstrap/ng-bootstrap";
import * as _ from "lodash";
import { ISession } from "src/app/services/ApiService/SessionService/session";
import { SessionService } from "src/app/services/ApiService/SessionService/session.service";

@Component({
  selector: "app-session-edit-model",
  templateUrl: "./session-edit-model.component.html",
  styleUrls: ["./session-edit-model.component.scss"],
})
export class SessionEditModelComponent {
  date: string;
  placement = "bottom";

  selectedSession: ISession;

  sessionName: string;
  conductedBy: string;
  conductedOn: string;
  isUpdated: boolean = false;
  error: string = null;

  constructor(
    private modalService: NgbModal,
    private sessionService: SessionService
  ) {}

  @ViewChild("editContent", { static: false })
  editModalContent: any;

  @Output("getAllSessions") getAllSessions: EventEmitter<any> =
    new EventEmitter();

  ngbModalOptions: NgbModalOptions = {
    backdrop: "static",
    centered: true,
    keyboard: false,
  };

  dismissModal() {
    this.modalService.dismissAll();
    this.error = null;
    this.isUpdated = false;
  }
  closeModal() {
    this.modalService.dismissAll();
    this.error = null;
    this.isUpdated = false;
  }
  openVerticallyCentered(session: ISession) {
    this.selectedSession = session;
    this.sessionName = session.name;
    this.conductedBy = session.conductedBy;
    this.conductedOn = session.conductedOn;
    this.modalService.open(this.editModalContent, this.ngbModalOptions);
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
  onDateChange(e: NgbDate) {
    this.conductedOn = e.year + "-" + e.month + "-" + e.day;
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
          this.getAllSessions.emit();
        }
      },
      (error) => {
        this.error = error;
        this.isUpdated = false;
      }
    );
  }
}
