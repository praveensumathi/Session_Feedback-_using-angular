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
  public sessionId: number;

  constructor(
    private questionTemplateService: QuestionTemplateServiceService,
    private sessionService: SessionService,
    private questionService: QuestionService,
    private route: ActivatedRoute
  ) {
    this.sessionId = parseInt(this.route.snapshot.paramMap.get("id"));
  }

  ngOnInit() {
    this.GetAllSessions();
    this.GetQuestionsBySessionId();
    this.GetAllQuestionTemplates();
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
      this.session = !_.isEmpty(sessions)
        ? sessions.find((x) => x.id === this.sessionId)
        : null;
    });
  }

  GetQuestionsBySessionId() {
    this.questionService
      .GetQuestionsBySessionId(this.sessionId)
      .subscribe((questions) => {
        this.questions = questions.map((question) => {
          return {
            ...question,
            createdOn: new Date(question.createdOn),
          };
        });
      });
  }
}
