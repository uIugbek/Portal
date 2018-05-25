import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { NewsCategory } from '@core/models';
import { NewsCategoryService } from '@core/services';
import { BaseKendoGridComponent } from '@core/components/base/base-kendo-grid.component';

@Component({
  selector: 'app-news-category-list',
  templateUrl: './news-category-list.component.html',
  styleUrls: ['./news-category-list.component.scss'],
  providers: [NewsCategoryService]
})
export class NewsCategoryListComponent extends BaseKendoGridComponent<
NewsCategory,
NewsCategoryService
> {
constructor(
  public dataService: NewsCategoryService,
  public translate: TranslateService
) {
  super(dataService, translate);
}
}
