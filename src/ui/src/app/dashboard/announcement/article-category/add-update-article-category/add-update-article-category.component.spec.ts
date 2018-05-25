import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdateArticleCategoryComponent } from './add-update-article-category.component';

describe('AddUpdateArticleCategoryComponent', () => {
  let component: AddUpdateArticleCategoryComponent;
  let fixture: ComponentFixture<AddUpdateArticleCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddUpdateArticleCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUpdateArticleCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
