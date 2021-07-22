import {
  Component,
  EventEmitter,
  OnInit,
  Output,
  ViewChild,
} from "@angular/core";
import {
  NgbDate,
  NgbModal,
  NgbModalOptions,
  NgbTimeAdapter,
  NgbTimepickerConfig,
  NgbTimeStruct,
} from "@ng-bootstrap/ng-bootstrap";
import * as _ from "lodash";
import { pad } from "lodash";
import { ISession } from "src/app/services/ApiService/SessionService/session";
import { SessionService } from "src/app/services/ApiService/SessionService/session.service";

@Component({
  selector: "app-session-add-modal",
  templateUrl: "./session-add-modal.component.html",
  styleUrls: ["./session-add-modal.component.scss"],
})
export class SessionAddModalComponent {
  isAdded: boolean = false;
  isValidSession: boolean = false;
  error: string = null;
  sessionName: string = null;
  createdBy: string = null;
  conductedOn: string = null;
  conductedBy: string = null;

  constructor(
    private modalService: NgbModal,
    private sessionService: SessionService,
    public config: NgbTimepickerConfig
  ) {
    config.seconds = false;
    config.spinners = false;
  }

  @ViewChild("addContent", { static: false })
  addModalContent: any;

  @Output("getAllSessions") getAllSessions: EventEmitter<any> =
    new EventEmitter();

  ngbModalOptions: NgbModalOptions = {
    backdrop: "static",
    centered: true,
    keyboard: false,
  };

  time: NgbTimeStruct;

  dismissModal() {
    this.modalService.dismissAll();
    this.isAdded = false;
    this.sessionName = null;
    this.createdBy = null;
    this.conductedOn = null;
    this.conductedBy = null;
  }
  closeModal() {
    this.modalService.dismissAll();
    this.isAdded = false;
    this.sessionName = null;
    this.createdBy = null;
    this.conductedOn = null;
    this.conductedBy = null;
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
  }

  pad = (i: number): string => (i < 10 ? `0${i}` : `${i}`);

  toModel(time: NgbTimeStruct | null): string | null {
    return time != null
      ? `${this.pad(time.hour)}:${this.pad(time.minute)}:${this.pad(
          time.second
        )}`
      : null;
  }

  addSession() {
    var session: ISession = {
      id: 0,
      name: this.sessionName,
      createdBy: this.createdBy,
      conductedBy: this.conductedBy,
      conductedOn: this.conductedOn + ` ${this.toModel(this.time)}`,
    };

    console.log(session);
    // this.sessionService.AddSession(session).subscribe(
    //   (addedSession) => {
    //     this.isAdded = true;
    //     this.getAllSessions.emit();
    //   },
    //   (error) => {
    //     this.error = error;
    //   }
    // );
  }
}
