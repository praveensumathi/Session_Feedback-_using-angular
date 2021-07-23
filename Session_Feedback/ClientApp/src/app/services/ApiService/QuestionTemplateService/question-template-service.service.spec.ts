import { TestBed } from '@angular/core/testing';

import { QuestionTemplateServiceService } from './question-template-service.service';

describe('QuestionTemplateServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: QuestionTemplateServiceService = TestBed.get(QuestionTemplateServiceService);
    expect(service).toBeTruthy();
  });
});
