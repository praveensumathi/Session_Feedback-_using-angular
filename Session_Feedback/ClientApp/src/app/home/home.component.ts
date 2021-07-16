import { Component, OnInit } from "@angular/core";
import { ISession } from "../services/ApiService/SessionService/session";
import { SessionService } from "../services/ApiService/SessionService/session.service";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { DeleteDialogContentComponent } from "./DialogContent/delete-dialog-content.component";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {
  public sessions: ISession[];

  constructor(private session: SessionService, public dialog: MatDialog) {}

  ngOnInit() {
    this.session
      .GetAllSession()
      .subscribe((sessions) => (this.sessions = sessions));
  }

  openDelete(session: ISession) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      session: session,
    };
    const dialogRef = this.dialog.open(
      DeleteDialogContentComponent,
      dialogConfig
    );

    dialogRef.afterClosed().subscribe((result) => {
      console.log(`Dialog result: ${result}`);
    });
  }

  edit(id: number) {}
}
