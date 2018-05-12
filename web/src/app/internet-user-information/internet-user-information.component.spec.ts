import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InternetUserInformationComponent } from './internet-user-information.component';

describe('InternetUserInformationComponent', () => {
  let component: InternetUserInformationComponent;
  let fixture: ComponentFixture<InternetUserInformationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InternetUserInformationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InternetUserInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
