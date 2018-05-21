import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateInterentuserDialogComponent } from './create-interentuser-dialog.component';

describe('CreateInterentuserDialogComponent', () => {
  let component: CreateInterentuserDialogComponent;
  let fixture: ComponentFixture<CreateInterentuserDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateInterentuserDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateInterentuserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
