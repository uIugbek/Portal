import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdateNewsCategoryComponent } from './add-update-news-category.component';

describe('AddUpdateNewsCategoryComponent', () => {
  let component: AddUpdateNewsCategoryComponent;
  let fixture: ComponentFixture<AddUpdateNewsCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddUpdateNewsCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUpdateNewsCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
