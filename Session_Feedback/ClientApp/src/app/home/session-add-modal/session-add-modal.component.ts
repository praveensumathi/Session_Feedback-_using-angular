import { Component, OnInit, ViewChild } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { SessionService } from "src/app/services/ApiService/SessionService/session.service";

@Component({
  selector: "app-session-add-modal",
  templateUrl: "./session-add-modal.component.html",
  styleUrls: ["./session-add-modal.component.css"],
})
export class SessionAddModalComponent {
  isAdded: boolean = false;
  error: string = null;

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
}
