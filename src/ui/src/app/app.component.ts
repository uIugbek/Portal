import { Component } from '@angular/core';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';

import { Configuration } from './app.configuration';
import { Constant } from './app.constant';
import { Culture } from '@core/models';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(
    public translate: TranslateService,
    public configuration: Configuration
  ) {

    this.translate.setDefaultLang(Constant.defaultLang);
    this.translate.use(Constant.currentLang());

    if (localStorage.getItem(Constant.LANGS_KEY)) {
      Constant.cultures = Observable.of(JSON.parse(localStorage.getItem(Constant.LANGS_KEY)));
    }
    else {
      Constant.cultures = this.configuration.getCultures();
      this.configuration.getCultures().subscribe(
        data => {
          this.translate.addLangs(data.map((culture: Culture) => culture.code));
          localStorage.setItem(Constant.LANG_KEY, JSON.stringify(data.find(f => f.code == Constant.currentLang())));
          localStorage.setItem(Constant.LANGS_KEY, JSON.stringify(data));
          Constant.cultures = Observable.of(JSON.parse(localStorage.getItem(Constant.LANGS_KEY)));
        }
      );
    }

  }
}
