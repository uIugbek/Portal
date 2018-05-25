import { Component, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { BaseKendoGridComponent } from '@core/components';
import { CultureService } from '@core/services';
import { Culture } from '@core/models';
import { Configuration } from "app/app.configuration";

@Component({
  selector: 'app-culture-list',
  templateUrl: './culture-list.component.html',
  styleUrls: ['./culture-list.component.scss'],
  providers: [CultureService]
})
export class CultureListComponent extends BaseKendoGridComponent<
Culture,
CultureService
> {
  constructor(
    public dataService: CultureService,
    public translate: TranslateService,
    public configuration: Configuration
  ) {
    super(dataService, translate);
  }

  onDelete() {
    super.onDelete();
    this.configuration.reLoadCultures();
  }
}
