import { TranslateService } from '@ngx-translate/core';
import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { BaseAddUpdateComponent } from '@core/components';
import { City } from '@core/models';
import { Configuration } from 'app/app.configuration';
import { CityService } from '@core/services';
import { Constant } from 'app/app.constant';
declare let $: any;
@Component({
  selector: 'app-add-update-city',
  templateUrl: './add-update-city.component.html',
  styleUrls: ['./add-update-city.component.scss'],
  providers: [CityService]
})
export class AddUpdateCityComponent
  extends BaseAddUpdateComponent<City, CityService>
  implements OnInit, AfterViewInit {
  public cultures = Constant.cultures;

  constructor(
    public location: Location,
    public route: ActivatedRoute,
    public configuration: Configuration,
    public dataService: CityService,
    public translate: TranslateService
  ) {
    super(dataService, location, route);
    this.entity = new City();

  }

  ngOnInit() {
    super.ngOnInit();
    this.initialize();

  }


  initialize() {
    this.route.params.subscribe(params => {
      this.entity.regionId = +params['regionId'];
    });
  }

  ngAfterViewInit() {
    Constant.cultures.subscribe(data =>
      data.forEach((element, index) => {
        $('#summernote_' + index).summernote();
      })
    );
  }

  onGetByIdComplete() {
    super.onGetByIdComplete();
    Constant.cultures.subscribe(data =>
      data.forEach((element, index) => {
        $('#summernote_' + index).summernote(
          'code',
          this.entity.localizations[index].description
        );
      })
    );
  }

  addOrUpdate() {
    Constant.cultures.subscribe(data =>
      data.forEach((element, index) => {
        let code = $('#summernote_' + index).summernote('code');
        this.entity.localizations[index].description = code as string;
      })
    );
    super.addOrUpdate();
  }
}
