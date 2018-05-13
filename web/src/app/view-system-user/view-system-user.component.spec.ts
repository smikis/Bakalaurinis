import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewSystemUserComponent } from './view-system-user.component';

describe('ViewSystemUserComponent', () => {
  let component: ViewSystemUserComponent;
  let fixture: ComponentFixture<ViewSystemUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewSystemUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewSystemUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
