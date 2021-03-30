import { TestBed } from '@angular/core/testing';

import { ApicommsService } from './apicomms.service';

describe('ApicommsService', () => {
  let service: ApicommsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApicommsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
