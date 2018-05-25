import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { BaseEntityService } from '@core/services/base/base-entity.service';
import { AuthService } from "@core/services/account/auth.service";
import { SelectList } from '@core/models';
import { Culture } from '../../models';
import { Constant } from 'app/app.constant';

@Injectable()
export class CultureService extends BaseEntityService<Culture> {
  constructor(
    public http: HttpClient,
    public authService: AuthService,
    public translate: TranslateService
  ) {
    super(http, translate, authService, '/dashboard/culture');
  }
  
}
