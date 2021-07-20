import { AfterViewInit, Component, ViewChild } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { ISession } from "src/app/services/ApiService/SessionService/session";

@Component({
  selector: "app-session-edit-model",
  templateUrl: "./session-edit-model.component.html",
  styleUrls: ["./session-edit-model.component.css"],
})
export class SessionEditModelComponent implements AfterViewInit {
  private editModelContent: any;
  public selectedSession: ISession;

  constructor(private modalService: NgbModal) {}

  @ViewChild("content", { static: false })
  editModel: any;

  ngAfterViewInit() {
    debugger;
    this.editModelContent = this.editModel;
    console.log(this.editModel);
  }

  dismissModal() {
    debugger;
    this.modalService.dismissAll();
  }
  closeModal() {
    this.modalService.dismissAll();
  }
  openVerticallyCentered(session: ISession) {
    debugger;
    this.selectedSession = session;
    this.modalService.open(this.editModelContent, { centered: true });
  }
}
