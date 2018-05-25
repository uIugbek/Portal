import { Component, Input, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { BaseKendoGridComponent } from '@core/components';
import { Region, Country } from '@core/models';
import { RegionService } from '@core/services';

@Component({
  selector: 'app-region-list',
  templateUrl: './region-list.component.html',
  styleUrls: ['./region-list.component.scss'],
  providers: [RegionService]
})
export class RegionListComponent extends BaseKendoGridComponent<
  Region,
  RegionService
> implements OnInit {
  @Input() public country: Country;

  constructor(
    public dataService: RegionService,
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
        if (this.country !== undefined) {
          this.gridData.data = this.gridData.data.filter((value: Region) => {
            return value.countryId === this.country.id;
          });
        }
      }
    );
  }
}
