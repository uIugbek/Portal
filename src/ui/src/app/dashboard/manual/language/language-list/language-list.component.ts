import { Component, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { BaseKendoGridComponent } from '@core/components';
import { LanguageService } from '@core/services';
import { Language } from '@core/models';

@Component({
  selector: 'app-language-list',
  templateUrl: './language-list.component.html',
  styleUrls: ['./language-list.component.scss'],
  providers: [LanguageService]
})
export class LanguageListComponent extends BaseKendoGridComponent<
Language,
LanguageService
> {
  constructor(
    public dataService: LanguageService,
    public translate: TranslateService
  ) {
    super(dataService, translate);
  }
}
