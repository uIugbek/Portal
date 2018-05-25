import { HttpClient } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { SelectList } from '@core/models';
import { BaseEntityService } from "@core/services/base/base-entity.service";
import { AuthService } from "@core/services/account/auth.service";
import { Constant } from 'app/app.constant';
import { Country } from '../../models';

@Injectable()
export class CountryService extends BaseEntityService<Country> {
  constructor(
    public http: HttpClient,
    public authService: AuthService,
    public translate: TranslateService
  ) {
    super(http, translate, authService, '/dashboard/country');
  }
}
