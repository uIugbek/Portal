import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';


import { SelectList } from '@core/models';
import { BaseEntityService } from '@core/services/base/base-entity.service';
import { AuthService } from "@core/services/account/auth.service";
import { NewsCategory } from '../../models/announcement';
import { Constant } from 'app/app.constant';

@Injectable()
export class NewsCategoryService extends BaseEntityService<NewsCategory> {
  constructor(
    public http: HttpClient,
    public authService: AuthService,
    public translate: TranslateService
  ) {
    super(http, translate, authService, '/dashboard/newsCategory');
  }

  getCodeList(): Observable<SelectList[]> {
    return this.http.get<SelectList[]>(
      Constant.server + '/api/' + Constant.currentLang() + '/manual/GetArticleCategoryCode'
    );
  }

  getManualNewsCategories(): Observable<NewsCategory[]> {
    return this.http.get<NewsCategory[]>(
      Constant.server + '/api/' + Constant.currentLang() + '/manual/GetNewsCategories'
    );
  }
}
