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
  NgbTimepickerConfig,
  NgbTimeStruct,
} from "@ng-bootstrap/ng-bootstrap";
import * as _ from "lodash";
import { ISession } from "src/app/services/ApiService/SessionService/session";
import { SessionService } from "src/app/services/ApiService/SessionService/session.service";

@Component({
  selector: "app-session-edit-model",
  templateUrl: "./session-edit-model.component.html",
  styleUrls: ["./session-edit-model.component.scss"],
})
export class SessionEditModelComponent {
  datePickerPlacement = "top";

  selectedSession: ISession;
  sessionName: string;
  conductedBy: string;
  conductedOn: string;
  conductedTime: string;
  isUpdated: boolean = false;
  error: string = null;

  constructor(
    private modalService: NgbModal,
    private sessionService: SessionService,
    public config: NgbTimepickerConfig
  ) {
    config.seconds = false;
    config.spinners = false;
  }

  @ViewChild("editContent", { static: false })
  editModalContent: any;

  @Output("getAllSessions") getAllSessions: EventEmitter<any> =
    new EventEmitter();

  ngbModalOptions: NgbModalOptions = {
    backdrop: "static",
    centered: true,
    keyboard: false,
  };
  sessionTime: NgbTimeStruct;

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
    this.conductedOn = session.conductedOn
      ? new Date(session.conductedOn).toLocaleDateString()
      : null;
    this.sessionTime = this.fromModel(
      session.conductedOn
        ? new Date(session.conductedOn).toLocaleTimeString()
        : null
    );
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

  fromModel(value: string | null): NgbTimeStruct | null {
    if (!value) {
      return null;
    }
    const split = value.split(":");
    return {
      hour: parseInt(split[0], 10),
      minute: parseInt(split[1], 10),
      second: parseInt(split[2], 10),
    };
  }

  updatedSession() {
    var updateSession: ISession = {
      ...this.selectedSession,
      name: this.sessionName,
      conductedBy: this.conductedBy,
      conductedOn: new Date(this.conductedOn),
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
