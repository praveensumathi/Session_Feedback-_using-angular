import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import * as _ from "lodash";
import { ISession } from "src/app/services/ApiService/SessionService/session";
import { SessionService } from "src/app/services/ApiService/SessionService/session.service";

@Component({
  selector: "app-update-dialog",
  templateUrl: "./update-dialog.component.html",
  styleUrls: ["./update-dialog.component.css"],
})
export class UpdateDialogComponent implements OnInit {
  sessionName: string;
  conductedBy: string;
  conductedOn: Date;
  currentSession: ISession;
  isUpdated: boolean = false;
  isLoading: boolean = false;
  error: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) data,
    private sessionService: SessionService
  ) {
    this.currentSession = data.session;
    this.sessionName = data.session.name;
    this.conductedBy = data.session.conductedBy;
    this.conductedOn = data.session.conductedOn;
  }

  ngOnInit() {}

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

  updateSession() {
    var updateSession: ISession = {
      ...this.currentSession,
      name: this.sessionName,
      conductedBy: this.conductedBy,
      conductedOn: this.conductedOn,
    };
    console.log(updateSession);
    this.isLoading = true;
    this.sessionService.UpdateSession(updateSession).subscribe(
      (isUpdated) => {
        if (isUpdated) {
          this.isUpdated = isUpdated;
          this.isLoading = false;
        }
      },
      (error) => {
        this.error = error;
        this.isUpdated = false;
        this.isLoading = false;
      }
    );
  }
}
