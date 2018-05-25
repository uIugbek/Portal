import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { BaseKendoGridComponent } from '@core/components';
import { ArticleCategory } from '@core/models';
import { ArticleCategoryService } from '@core/services';

@Component({
  selector: 'app-article-category-list',
  templateUrl: './article-category-list.component.html',
  styleUrls: ['./article-category-list.component.scss'],
  providers: [ArticleCategoryService]
})
export class ArticleCategoryListComponent extends BaseKendoGridComponent<
ArticleCategory,
ArticleCategoryService
> {
constructor(
  public dataService: ArticleCategoryService,
  public translate: TranslateService
) {
  super(dataService, translate);
}
}
