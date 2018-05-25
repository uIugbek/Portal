import { TranslateService } from '@ngx-translate/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { SelectList } from '@core/models';
import { BaseEntityService } from '@core/services/base/base-entity.service';
import { AuthService } from '@core/services/account/auth.service';
import { Configuration } from 'app/app.configuration';
import { Constant } from 'app/app.constant';
import { User } from '../../models';

@Injectable()
export class UserService extends BaseEntityService<User> {
  constructor(
    public http: HttpClient,
    public authService: AuthService,
    public translate: TranslateService
  ) {
    super(http, translate, authService, '/dashboard/user');
  }

  getAges(): Observable<SelectList[]> {
    return this.http.get<SelectList[]>(
      Constant.server +
        '/api/' +
        this.translate.currentLang +
        '/manual/GetEnum' +
        '?enumName=Age'
    );
  }
}
