import { Component, OnInit, ViewChild } from "@angular/core";
import { NgbDate, NgbModal, NgbModalOptions } from "@ng-bootstrap/ng-bootstrap";
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
  sessionName: string = null;
  createdBy: string = null;
  conductedOn: string = null;
  conductedBy: string = null;

  constructor(
    private modalService: NgbModal,
    private sessionService: SessionService
  ) {}

  @ViewChild("addContent", { static: false })
  addModalContent: any;

  ngbModalOptions: NgbModalOptions = {
    backdrop: "static",
    centered: true,
  };

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
    this.modalService.open(this.addModalContent, this.ngbModalOptions);
  }

  isValidSessionDetails() {
    if (_.isNil(this.sessionName) || _.isNil(this.createdBy)) {
      return (this.isValidSession = false);
    } else {
      return (this.isValidSession = true);
    }
  }
  onNameChange(e) {
    if (_.isEmpty(e)) {
      this.sessionName = null;
    }
  }
  onCreatedByChange(e) {
    if (_.isEmpty(e)) {
      this.createdBy = null;
    }
  }
  onConductedByChange(e) {
    this.conductedBy = e;
  }
  onConductedOnChange(e: NgbDate) {
    this.conductedOn = e.year + "-" + e.month + "-" + e.day;
    this.isValidSession = false;
  }
}
