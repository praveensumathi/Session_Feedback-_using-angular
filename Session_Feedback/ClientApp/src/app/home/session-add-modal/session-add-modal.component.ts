import { Component, OnInit, ViewChild } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import * as _ from "lodash";
import { ISession } from "src/app/services/ApiService/SessionService/session";
import { SessionService } from "src/app/services/ApiService/SessionService/session.service";

@Component({
  selector: "app-session-add-modal",
  templateUrl: "./session-add-modal.component.html",
  styleUrls: ["./session-add-modal.component.css"],
})
export class SessionAddModalComponent {
  isAdded: boolean = false;
  isValidSession: boolean = false;
  error: string = null;
  newSession: ISession;
  sessionName: string;
  createdBy: string;
  conductedOn: string;
  conductedBy: string;

  constructor(
    private modalService: NgbModal,
    private sessionService: SessionService
  ) {}

  @ViewChild("addContent", { static: false })
  addModalContent: any;

  dismissModal() {
    this.modalService.dismissAll();
    this.error = null;
    this.isAdded = false;
  }
  closeModal() {
    this.modalService.dismissAll();
    this.error = null;
    this.isAdded = false;
  }

  openVerticallyCentered() {
    this.modalService.open(this.addModalContent, { centered: true });
  }

  isValidSessionDetails() {
    if (
      !_.isEmpty(this.newSession.name) ||
      !_.isEmpty(this.newSession.createdBy)
    ) {
      this.isValidSession = true;
    }
  }

  onNameChange(e) {
    if (_.isEmpty(e)) {
      this.isValidSession = false;
    }
  }
  onCreatedByChange(e) {}
  onConductedByChange(e) {}
  onConductedOnChange(e) {}
}
