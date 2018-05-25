import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { SelectList } from '@core/models';
import { BaseEntityService } from '@core/services/base/base-entity.service';
import { AuthService } from "@core/services/account/auth.service";
import { Configuration } from 'app/app.configuration';
import { Role } from '../../models';

@Injectable()
export class RoleService extends BaseEntityService<Role> {
  constructor(
    public http: HttpClient,
    public authService: AuthService,
    public translate: TranslateService
  ) {
    super(http, translate, authService, '/dashboard/role');
  }

  getAllNames(): Observable<string[]>{
    return this.http.get<string[]>(this.baseUrl + this.translate.currentLang + this.actionUrl + '/GetRolesNames');
  }
}
