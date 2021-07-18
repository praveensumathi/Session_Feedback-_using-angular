import { Component, Inject, OnInit } from "@angular/core";
import {
  MatDialog,
  MatDialogConfig,
  MAT_DIALOG_DATA,
} from "@angular/material/dialog";
import { ISession } from "src/app/services/ApiService/SessionService/session";
import { SessionService } from "src/app/services/ApiService/SessionService/session.service";

@Component({
  selector: "session-delete-dialog-content",
  templateUrl: "./delete-dialog.component.html",
})
export class DeleteDialogComponent implements OnInit {
  session: ISession;
  isDeleted: boolean = false;
  isLoading: boolean = false;
  error: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) data,
    private sessionService: SessionService
  ) {
    this.session = data.session;
  }

  ngOnInit(): void {}

  deleteSession(id: number) {
    this.isLoading = true;
    this.sessionService.DeleteSession(id).subscribe(
      (isDelete) => {
        if (isDelete) {
          this.isDeleted = isDelete;
          this.isLoading = false;
        }
      },
      (error) => {
        this.error = error;
        this.isDeleted = false;
        this.isLoading = false;
      }
    );
  }
}
