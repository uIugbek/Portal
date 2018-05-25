import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { SelectList, Article } from '@core/models';
import { BaseKendoGridComponent } from '@core/components';

import { ArticleService } from '@core/services';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.scss'],
  providers: [ArticleService]
})
export class ArticleListComponent extends BaseKendoGridComponent<
Article,
ArticleService
> implements OnInit {
public regions: SelectList[];

constructor(
  public dataService: ArticleService,
  public translate: TranslateService
) {
  super(dataService, translate);
}

ngOnInit() {
  super.ngOnInit();
}
}
