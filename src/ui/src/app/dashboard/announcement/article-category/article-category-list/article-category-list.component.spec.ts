import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleCategoryListComponent } from './article-category-list.component';

describe('ArticleCategoryListComponent', () => {
  let component: ArticleCategoryListComponent;
  let fixture: ComponentFixture<ArticleCategoryListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArticleCategoryListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleCategoryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
