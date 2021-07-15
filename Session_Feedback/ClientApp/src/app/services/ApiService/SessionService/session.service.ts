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

  AddSession(session: ISession) {}
  UpdateSession(session: ISession) {}
  DeleteSession(id: number) {}
}