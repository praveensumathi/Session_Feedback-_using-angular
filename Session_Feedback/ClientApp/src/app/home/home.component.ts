import { Component, OnInit } from "@angular/core";
import { ISession } from "../services/ApiService/SessionService/session";
import { SessionService } from "../services/ApiService/SessionService/session.service";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { DeleteDialogComponent } from "./DialogContent/DeleteDialog/delete-dialog.component";
import { UpdateDialogComponent } from "./DialogContent/UpdateDiaog/update-dialog.component";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {
  public sessions: ISession[];

  constructor(private session: SessionService, public dialog: MatDialog) {}

  ngOnInit() {
    this.GetAllSessions();
  }

  GetAllSessions() {
    this.session
      .GetAllSession()
      .subscribe((sessions) => (this.sessions = sessions));
  }

  openDelete(session: ISession) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      session: session,
    };

    const dialogRef = this.dialog.open(DeleteDialogComponent, dialogConfig);

    dialogRef.beforeClosed().subscribe((result) => {
      if (result) {
        this.GetAllSessions();
      }
    });
  }

  openEdit(session: ISession) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      session: session,
    };

    dialogConfig.width = "30vw";
    const dialogRef = this.dialog.open(UpdateDialogComponent, dialogConfig);

    dialogRef.beforeClosed().subscribe((result) => {
      if (result) {
        this.GetAllSessions();
      }
    });
  }
}
