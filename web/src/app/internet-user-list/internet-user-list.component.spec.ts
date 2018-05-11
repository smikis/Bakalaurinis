import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InternetUserListComponent } from './internet-user-list.component';

describe('InternetUserListComponent', () => {
  let component: InternetUserListComponent;
  let fixture: ComponentFixture<InternetUserListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InternetUserListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InternetUserListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
