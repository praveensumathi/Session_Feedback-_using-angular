import { Component, OnInit } from "@angular/core";
import { NgSelectModule, NgOption } from "@ng-select/ng-select";
import { IQuestionTemplate } from "../services/ApiService/QuestionTeplateService/IQuestionTemplate";
import { QuestionTemplateServiceService } from "../services/ApiService/QuestionTeplateService/question-template-service.service";
import { ISession } from "../services/ApiService/SessionService/session";
import { SessionService } from "../services/ApiService/SessionService/session.service";

@Component({
  selector: "app-session-details",
  templateUrl: "./session-details.component.html",
  styleUrls: ["./session-details.component.scss"],
})
export class SessionDetailsComponent implements OnInit {
  public questionTemplates: IQuestionTemplate[];
  public sessions: ISession[];

  constructor(
    private questionTemplateService: QuestionTemplateServiceService,
    private session: SessionService
  ) {}

  ngOnInit() {
    this.GetAllQuestionTemplates();
    this.GetAllSessions();
  }

  GetAllQuestionTemplates() {
    this.questionTemplateService
      .GetAllTemplates()
      .subscribe(
        (questionTemplates) => (this.questionTemplates = questionTemplates)
      );
  }
  GetAllSessions() {
    this.session
      .GetAllSession()
      .subscribe((sessions) => (this.sessions = sessions));
  }
}
