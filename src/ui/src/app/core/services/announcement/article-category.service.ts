import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { BaseEntityService } from '@core/services/base/base-entity.service';
import { SelectList } from '@core/models';
import { AuthService } from "@core/services/account/auth.service";
import { ArticleCategory } from '../../models/announcement';
import { Constant } from 'app/app.constant';

@Injectable()
export class ArticleCategoryService extends BaseEntityService<ArticleCategory> {
  constructor(
    public http: HttpClient,
    public authService: AuthService,
    public translate: TranslateService
  ) {
    super(http, translate, authService, '/dashboard/articleCategory');
  }

  // getCodeList(): Observable<SelectList[]> {
  //   return this.http.get<SelectList[]>(
  //     Constant.server + '/api/manual/GetArticleCategoryCode'
  //   );
  // }

  getManualArticleCategories(): Observable<ArticleCategory[]> {
    return this.http.get<ArticleCategory[]>(
      Constant.server + '/api/' + Constant.currentLang() + '/manual/GetArticleCategories'
    );
  }
}
