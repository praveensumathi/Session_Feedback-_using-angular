import { Injectable } from "@angular/core";
import { ISession } from "./session";

@Injectable({
  providedIn: "root",
})
export class SessionService {
  constructor() {}

  GetAllSession() {}
  AddSession(session: ISession) {}
  UpdateSession(session: ISession) {}
  DeleteSession(id: number) {}
}
