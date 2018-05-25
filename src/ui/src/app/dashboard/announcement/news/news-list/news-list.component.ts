import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { SelectList } from '@core/models';
import { BaseKendoGridComponent } from '@core/components';
import { News } from '@core/models';
import { NewsService } from '@core/services';

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.scss'],
  providers: [NewsService]
})
export class NewsListComponent  extends BaseKendoGridComponent<
News,
NewsService
> implements OnInit {
public regions: SelectList[];

constructor(
  public dataService: NewsService,
  public translate: TranslateService
) {
  super(dataService, translate);
}

ngOnInit() {
  super.ngOnInit();
}
}
