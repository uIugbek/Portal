import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs/Rx';

import { BaseEntityService } from '@core/services/base/base-entity.service';
import { AuthService } from '@core/services/account/auth.service';
import { SelectList } from '@core/models';
import { Region } from '../../models';
import { Constant } from 'app/app.constant';

@Injectable()
export class RegionService extends BaseEntityService<Region> {
  constructor(
    public http: HttpClient,
    public authService: AuthService,
    public translate: TranslateService
  ) {
    super(http, translate, authService, '/dashboard/region');
  }

  getRegionsByCountryAsSelect(id: number): Observable<SelectList[]> {
    return this.http.get<SelectList[]>(
      Constant.server + '/api/manual/GetRegionsAsSelect/' + id
    );
  }

  getRegions(skip: number, take: number): Observable<Region[]> {
    return this.http.get<Region[]>(
      Constant.server + '/api' + '/' + this.translate.currentLang + `/region/GetRegions?`,
      {
        headers: this.getRequestHeaders(),
        params: {
          skip: skip.toString(),
          take: take.toString()
        }
      }
    );
  };
}
