import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { map } from "rxjs/internal/operators/map";
import { ResourceService } from "../../GenericResourceService/resource-service.service";
import { ISession } from "./session";

@Injectable({
  providedIn: "root",
})
export class SessionService {
  constructor(private resourceService: ResourceService<ISession>) {}

  fromServerModel(session: ISession): ISession {
    return {
      ...session,
      createdOn: new Date(session.createdOn),
      modifiedOn: new Date(session.modifiedOn),
      modifiedBy: session.modifiedBy,
      conductedOn: session.conductedOn,
    };
  }

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
}
