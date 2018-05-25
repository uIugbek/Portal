import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { SelectList, Culture } from '@core/models';
import { BaseAddUpdateComponent } from '@core/components';
import { Configuration } from 'app/app.configuration';
import { NewsCategory } from '@core/models';
import { 
  NewsCategoryService, 
  RegionService, 
  CityService 
} from '@core/services';
import { Constant } from 'app/app.constant';

@Component({
  selector: 'app-add-update-news-category',
  templateUrl: './add-update-news-category.component.html',
  styleUrls: ['./add-update-news-category.component.scss'],
  providers: [
    NewsCategoryService, 
    RegionService, 
    CityService, 
    NewsCategoryService
  ]
})
export class AddUpdateNewsCategoryComponent extends BaseAddUpdateComponent<
          NewsCategory, 
          NewsCategoryService> implements OnInit {

  public newsCategories: SelectList[];
  public regions: SelectList[];
  public cities: SelectList[];
  public cultures = Constant.cultures;

  constructor(
    public dataService: NewsCategoryService,
    public location: Location,
    public route: ActivatedRoute,
    public configuration: Configuration,
    public regionService: RegionService,
    public cityService: CityService,
    public newsCategoryService: NewsCategoryService
  ) {
    super(dataService, location, route);
    this.entity = new NewsCategory();
  }

  ngOnInit() {
    super.ngOnInit();
    this.newsCategoryService.getManualNewsCategories().subscribe(data => {
      this.newsCategories = data.map(s => {
        let model: SelectList = <SelectList>{
          value: s.id,
          text: s.name,
          selected: false
        };
        return model;
      });
    });
    this.regionService.getAsSelect().subscribe(data => {
      this.regions = data.map(s => {
        let model: SelectList = <SelectList>{
          value: s.value,
          text: s.text,
          selected: s.selected
        };
        return model;
      });
    });
  }

  regionChange(region: SelectList) {
    if (region != null) {
      this.cityService.getCitiesByRegion(region.value).subscribe(data => {
        this.cities = data.map(s => {
          let model: SelectList = <SelectList>{
            value: s.id,
            text: s.name,
            selected: false
          };
          return model;
        });
      });
    }
  }
}
