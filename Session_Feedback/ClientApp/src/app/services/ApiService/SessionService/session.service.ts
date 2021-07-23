import { Injectable } from "@angular/core";
import { NgbTimeStruct } from "@ng-bootstrap/ng-bootstrap";
import { Observable } from "rxjs/internal/Observable";
import { map } from "rxjs/internal/operators/map";
import { ResourceService } from "../../GenericResourceService/resource-service.service";
import { ISession } from "./session";

@Injectable({
  providedIn: "root",
})
export class SessionService {
  constructor(private resourceService: ResourceService<ISession>) {}

  GetAllSession(): Observable<ISession[]> {
    var result = this.resourceService
      .getAll("/api/session")
      .pipe(
        map((sessions) =>
          sessions.map((session) => this.fromServerModel(session))
        )
      );
    return result;
  }

  GetById(id: string): Observable<ISession> {
    var result = this.resourceService.getById("/api/session", { id });
    return result;
  }

  AddSession(session: ISession): Observable<ISession> {
    var result = this.resourceService.add("/api/session", session);
    return result;
  }

  UpdateSession(session: ISession) {
    var result = this.resourceService.update("/api/session", session);
    return result;
  }

  DeleteSession(id: number): Observable<boolean> {
    console.log(id);
    var result = this.resourceService.delete("/api/session", { id }, { id });
    return result;
  }

  fromServerModel(session: ISession): ISession {
    return {
      ...session,
      createdOn: new Date(session.createdOn),
      modifiedOn: new Date(session.modifiedOn),
      modifiedBy: session.modifiedBy,
      conductedOn: session.conductedOn,
    };
  }

  ngTmeStructFromModal(value: string | null): NgbTimeStruct | null {
    if (!value) {
      return null;
    }
    const split = value.split("T")[1].split(":");

    if (split[0] == "00" && split[1] == "00") {
      return null;
    }
    return {
      hour: parseInt(split[0], 10),
      minute: parseInt(split[1], 10),
      second: parseInt(split[2], 10),
    };
  }

  pad = (i: number): string => (i < 10 ? `0${i}` : `${i}`);

  ngTmeStructToModel(time: NgbTimeStruct | null): string | null {
    return time != null
      ? `${this.pad(time.hour)}:${this.pad(time.minute)}:${this.pad(
          time.second
        )}`
      : "";
  }
}
