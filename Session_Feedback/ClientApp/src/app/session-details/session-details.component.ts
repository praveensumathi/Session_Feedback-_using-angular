import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import * as _ from "lodash";
import { IQuestion } from "../services/ApiService/QuestionService/IQuestion";
import { QuestionService } from "../services/ApiService/QuestionService/question.service";
import { IQuestionTemplate } from "../services/ApiService/QuestionTemplateService/IQuestionTemplate";
import { QuestionTemplateServiceService } from "../services/ApiService/QuestionTemplateService/question-template-service.service";
import { ISession } from "../services/ApiService/SessionService/ISession";
import { SessionService } from "../services/ApiService/SessionService/session.service";

@Component({
  selector: "app-session-details",
  templateUrl: "./session-details.component.html",
  styleUrls: ["./session-details.component.scss"],
})
export class SessionDetailsComponent implements OnInit {
  public questionTemplates: IQuestionTemplate[];
  public sessions: ISession[];
  public questions: IQuestion[];
  public session: ISession;

  constructor(
    private questionTemplateService: QuestionTemplateServiceService,
    private sessionService: SessionService,
    private questionService: QuestionService,
    private activeRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.GetAllSessions();
    this.activeRoute.params.subscribe((routeParams) => {
      this.GetQuestionsBySessionId(Number(routeParams.id));
    });
    this.GetAllQuestionTemplates();
  }

  onRouterLinkChange(id: number) {
    this.session = !_.isEmpty(this.sessions)
      ? this.sessions.find((x) => x.id === id)
      : null;
  }

  GetAllQuestionTemplates() {
    this.questionTemplateService
      .GetAllTemplates()
      .subscribe(
        (questionTemplates) => (this.questionTemplates = questionTemplates)
      );
  }
  GetAllSessions() {
    this.sessionService.GetAllSession().subscribe((sessions) => {
      this.sessions = sessions;
    });
  }

  GetQuestionsBySessionId(id: number) {
    this.questionService.GetQuestionsBySessionId(id).subscribe((questions) => {
      this.onRouterLinkChange(id);
      this.questions = questions.map((question) => {
        return {
          ...question,
          createdOn: new Date(question.createdOn),
        };
      });
    });
  }
}
