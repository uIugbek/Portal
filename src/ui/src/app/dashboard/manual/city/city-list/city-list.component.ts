import { Component, OnInit, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { SelectList } from '@core/models';
import { BaseKendoGridComponent } from '@core/components';
import { CityService } from '@core/services';
import { City, Region } from '@core/models';

@Component({
  selector: 'app-city-list',
  templateUrl: './city-list.component.html',
  styleUrls: ['./city-list.component.scss'],
  providers: [CityService]
})
export class CityListComponent extends BaseKendoGridComponent<
  City,
  CityService
> implements OnInit {
  public regions: SelectList[];
  @Input() public region: Region;

  constructor(
    public dataService: CityService,
    public translate: TranslateService
  ) {
    super(dataService, translate);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  public getAll() {
    this.dataService.getAllAsDataResult(this.state).subscribe(
      data => {
        this.gridData = data;
      },
      error => console.log('& ' + error + ' &'),
      () => {
        if (this.region !== undefined) {
          this.gridData.data = this.gridData.data.filter((value: City) => {
            return value.regionId === this.region.id;
          });
        }
      }
    );
  }
}
