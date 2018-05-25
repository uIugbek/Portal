import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { SelectList } from '@core/models';
import { BaseEntityService } from '@core/services/base/base-entity.service';
import { AuthService } from '@core/services/account/auth.service';
import { City } from '../../models';
import { Constant } from 'app/app.constant';

@Injectable()
export class CityService extends BaseEntityService<City> {
  constructor(
    public http: HttpClient,
    public authService: AuthService,
    public translate: TranslateService
  ) {
    super(http, translate, authService, '/dashboard/city');
  }

  getCitiesByCountry(id: number): Observable<City[]> {
    return this.http.get<City[]>(
      Constant.server + '/api/' + this.translate.currentLang + 'manual/GetCitiesByCountry/' + id
    );
  }

  getCitiesByRegion(id: number): Observable<City[]> {
    return this.http.get<City[]>(
      Constant.server + '/api/' + this.translate.currentLang + '/manual/GetCities/' + id
    );
  }

  getCities(skip: number, take: number): Observable<City[]> {
    return this.http.get<City[]>(
      Constant.server + '/api' + '/' + this.translate.currentLang + `/city/GetCities?`,
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
