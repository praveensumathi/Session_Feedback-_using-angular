import { Component, OnInit, ViewChild } from "@angular/core";
import { NgbModal, NgbModalOptions } from "@ng-bootstrap/ng-bootstrap";
import { ISession } from "src/app/services/ApiService/SessionService/session";
import { SessionService } from "src/app/services/ApiService/SessionService/session.service";

@Component({
  selector: "app-session-delete-modal",
  templateUrl: "./session-delete-modal.component.html",
  styleUrls: ["./session-delete-modal.component.css"],
})
export class SessionDeleteModalComponent implements OnInit {
  selectedSession: ISession;
  isDeleted: boolean = false;
  error: string = null;

  constructor(
    private modalService: NgbModal,
    private sessionService: SessionService
  ) {}

  @ViewChild("deleteContent", { static: false })
  deleteModalContent: any;

  ngbModalOptions: NgbModalOptions = {
    backdrop: "static",
    centered: true,
  };

  ngOnInit() {}

  dismissModal() {
    this.modalService.dismissAll();
    this.error = null;
    this.isDeleted = false;
  }
  closeModal() {
    this.modalService.dismissAll();
    this.error = null;
    this.isDeleted = false;
  }

  openVerticallyCentered(session: ISession) {
    this.selectedSession = session;
    this.modalService.open(this.deleteModalContent, this.ngbModalOptions);
  }

  deleteSession() {
    this.sessionService.DeleteSession(this.selectedSession.id).subscribe(
      (isDeleted) => {
        this.isDeleted = isDeleted;
      },
      (error) => {
        this.error = error;
      }
    );
  }
}
