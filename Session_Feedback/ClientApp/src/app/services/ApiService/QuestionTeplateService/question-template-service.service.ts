import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { ResourceService } from "../../GenericResourceService/resource-service.service";
import { IQuestionTemplate } from "./IQuestionTemplate";

@Injectable({
  providedIn: "root",
})
export class QuestionTemplateServiceService {
  constructor(private resourceService: ResourceService<IQuestionTemplate>) {}

  GetAllTemplates(): Observable<IQuestionTemplate[]> {
    var result = this.resourceService.getAll("/api/questionTemplate");

    return result;
  }
}
