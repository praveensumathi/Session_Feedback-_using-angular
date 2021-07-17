import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ISession } from "src/app/services/ApiService/SessionService/session";
import { SessionService } from "src/app/services/ApiService/SessionService/session.service";

@Component({
  selector: "app-update-dialog",
  templateUrl: "./update-dialog.component.html",
  styleUrls: ["./update-dialog.component.css"],
})
export class UpdateDialogComponent implements OnInit {
  session: ISession;
  isUpdated: boolean = false;
  isLoading: boolean = false;
  error: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) data,
    private sessionService: SessionService
  ) {
    this.session = data.session;
  }

  ngOnInit() {}
}
