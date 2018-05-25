import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { BaseEntityService } from '@core/services/base/base-entity.service';
import { TranslateService } from '@ngx-translate/core';
import { Constant } from 'app/app.constant';
import { AuthService } from "@core/services/account/auth.service";
import { Article } from '../../models/announcement';

@Injectable()
export class ArticleService extends BaseEntityService<Article> {
  constructor(
    public http: HttpClient,
    public authService: AuthService,
    public translate: TranslateService
  ) {
    super(http, translate, authService, '/dashboard/article');
    // this.actionUrl = ;
  }

  getArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(Constant.server + '/api/' + Constant.currentLang() +'/manual/GetArticles');
  }

  getArticlesByArticleCategory(id: number): Observable<Article[]> {
    return this.http.get<Article[]>(
      Constant.server + '/api/' + Constant.currentLang() +'/manual/GetArticles/' + id
    );
  }
}
