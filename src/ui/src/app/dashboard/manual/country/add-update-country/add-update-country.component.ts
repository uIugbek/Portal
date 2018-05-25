import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

import { BaseAddUpdateComponent } from '@core/components';
import { CountryService } from '@core/services';
import { Country } from '@core/models';
import { Configuration } from 'app/app.configuration';
import { Constant } from 'app/app.constant';

@Component({
  selector: 'app-add-update-country',
  templateUrl: './add-update-country.component.html',
  styleUrls: ['./add-update-country.component.scss'],
  providers: [CountryService]
})
export class AddUpdateCountryComponent extends BaseAddUpdateComponent<
Country,
CountryService
> implements OnInit {
  public cultures = Constant.cultures;

  constructor(
    public dataService: CountryService,
    public location: Location,
    public route: ActivatedRoute,
    public configuration: Configuration,
    public translate: TranslateService
  ) {
    super(dataService, location, route);
    this.entity = new Country();
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
