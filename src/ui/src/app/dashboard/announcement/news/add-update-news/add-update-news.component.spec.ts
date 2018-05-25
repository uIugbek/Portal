import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdateNewsComponent } from './add-update-news.component';

describe('AddUpdateNewsComponent', () => {
  let component: AddUpdateNewsComponent;
  let fixture: ComponentFixture<AddUpdateNewsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddUpdateNewsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUpdateNewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
