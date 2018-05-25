import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs/Observable';

import { Constant } from './app.constant';
import { Culture } from '@core/models';

@Injectable()
export class Configuration {

  constructor(
    public http: HttpClient,
    public translate: TranslateService
  ) { }

  load(): void {
  }

  reLoadCultures(): void {
    this.getCultures().subscribe(
      data => {
        this.translate.addLangs(data.map((culture: Culture) => culture.code));
        localStorage.removeItem(Constant.LANGS_KEY);
        localStorage.removeItem(Constant.LANG_KEY);
        localStorage.setItem(Constant.LANGS_KEY, JSON.stringify(data));
        localStorage.setItem(Constant.LANG_KEY, JSON.stringify(data.find(f => f.code == Constant.currentLang())));
        Constant.cultures = Observable.of(JSON.parse(localStorage.getItem(Constant.LANGS_KEY)));
      }
    );
  }

  getCultures(): Observable<Culture[]> {
    return this.http
      .get<Culture[]>(Constant.server + '/api' + '/' + Constant.currentLang() + '/manual/GetCultures');
  }
}
