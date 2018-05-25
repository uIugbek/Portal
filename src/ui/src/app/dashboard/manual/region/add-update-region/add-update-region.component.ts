import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Component, OnInit, AfterViewInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { BaseAddUpdateComponent } from '@core/components';
import { Configuration } from 'app/app.configuration';
import { RegionService } from '@core/services';
import { Region } from '@core/models';
import { Constant } from 'app/app.constant';
declare let $: any;
@Component({
  templateUrl: './add-update-region.component.html',
  styleUrls: ['./add-update-region.component.scss'],
  providers: [RegionService]
})
export class AddUpdateRegionComponent
  extends BaseAddUpdateComponent<Region, RegionService>
  implements OnInit, AfterViewInit {
  public cultures = Constant.cultures;

  constructor(
    public dataService: RegionService,
    public location: Location,
    public configuration: Configuration,
    public route: ActivatedRoute,
    public translate: TranslateService
  ) {
    super(dataService, location, route);
    this.entity = new Region();
  }

  ngOnInit() {
    super.ngOnInit();
    this.initialize();
  }

  initialize() {
    this.route.params.subscribe(params => {
      this.entity.countryId = +params['countryId'];
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
