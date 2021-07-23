import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { ResourceService } from "../../GenericResourceService/resource-service.service";
import { IQuestion } from "./IQuestion";

@Injectable({
  providedIn: "root",
})
export class QuestionService {
  constructor(private resourceService: ResourceService<IQuestion>) {}

  GetQuestionsBySessionId(sessionId: number): Observable<IQuestion[]> {
    var result = this.resourceService.getAll(`/api/question/${sessionId}`);
    return result;
  }
}
