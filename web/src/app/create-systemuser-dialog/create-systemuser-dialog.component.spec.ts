import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSystemuserDialogComponent } from './create-systemuser-dialog.component';

describe('CreateSystemuserDialogComponent', () => {
  let component: CreateSystemuserDialogComponent;
  let fixture: ComponentFixture<CreateSystemuserDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateSystemuserDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateSystemuserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
