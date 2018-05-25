import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { BaseEntityService } from '@core/services/base/base-entity.service';
import { TranslateService } from '@ngx-translate/core';
import { Constant } from 'app/app.constant';
import { AuthService } from "@core/services/account/auth.service";
import { News } from '../../models/announcement';

@Injectable()
export class NewsService extends BaseEntityService<News> {
  constructor(
    public http: HttpClient,
    public authService: AuthService,
    public translate: TranslateService
  ) {
    super(http, translate, authService, '/dashboard/news');
    // this.actionUrl = ;
  }
}
