export interface IQuestion {
  id: number;
  feedback: string;
  createdBy: string;
  createdOn: Date;
  modifiedOn?: Date;
  modifiedBy?: string;
  sessionId: number;
}
