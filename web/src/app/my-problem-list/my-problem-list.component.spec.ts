import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyProblemListComponent } from './my-problem-list.component';

describe('MyProblemListComponent', () => {
  let component: MyProblemListComponent;
  let fixture: ComponentFixture<MyProblemListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyProblemListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyProblemListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
