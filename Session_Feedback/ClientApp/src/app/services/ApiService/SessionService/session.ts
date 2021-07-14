export interface ISession {
  id: number;
  feedbackAnswer: string;
  answeredBy: string;
  answeredOn: Date;
  userId: number;
  questionId: number;
}
