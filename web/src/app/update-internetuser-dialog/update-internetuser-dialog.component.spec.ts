import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateInternetuserDialogComponent } from './update-internetuser-dialog.component';

describe('UpdateInternetuserDialogComponent', () => {
  let component: UpdateInternetuserDialogComponent;
  let fixture: ComponentFixture<UpdateInternetuserDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdateInternetuserDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateInternetuserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
