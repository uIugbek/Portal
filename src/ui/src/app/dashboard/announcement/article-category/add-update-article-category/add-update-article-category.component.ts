import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { SelectList } from '@core/models';
import { BaseAddUpdateComponent } from '@core/components';
import { ArticleCategory } from '@core/models';
import { ArticleCategoryService } from '@core/services';
import { Configuration } from 'app/app.configuration';
import { Constant } from 'app/app.constant';

@Component({
  selector: 'app-add-update-article-category',
  templateUrl: './add-update-article-category.component.html',
  styleUrls: ['./add-update-article-category.component.scss'],
  providers: [ArticleCategoryService]
})
export class AddUpdateArticleCategoryComponent extends BaseAddUpdateComponent
<ArticleCategory, ArticleCategoryService> {

  public articleCategoryCodeList: SelectList[];
  public cultures = Constant.cultures;

  constructor(
    public dataService: ArticleCategoryService,
    public location: Location,
    public route: ActivatedRoute,
    public configuration: Configuration
  ) {
    super(dataService, location, route);
    this.entity = new ArticleCategory();
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
