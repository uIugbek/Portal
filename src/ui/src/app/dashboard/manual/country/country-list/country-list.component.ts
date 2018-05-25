import { Component, OnInit, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { BaseKendoGridComponent } from '@core/components';
import { CountryService } from '@core/services';
import { Country } from '@core/models';

@Component({
  selector: 'app-country-list',
  templateUrl: './country-list.component.html',
  styleUrls: ['./country-list.component.scss'],
  providers: [CountryService]
})
export class CountryListComponent extends BaseKendoGridComponent<
  Country,
  CountryService
> {
  constructor(
    public dataService: CountryService,
    public translate: TranslateService
  ) {
    super(dataService, translate);
  }
}
