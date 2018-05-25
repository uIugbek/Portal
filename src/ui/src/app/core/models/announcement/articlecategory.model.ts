import {
  BaseLocalizableEntity,
  BaseLocalizableEntity_Locale
} from '../base';
import { Constant } from 'app/app.constant';

export class ArticleCategory extends BaseLocalizableEntity<ArticleCategory_Locales> {
  public code: string;
  public name: string;
  public localizations: Array<ArticleCategory_Locales> = [];

  constructor() {
    super();

    this.localizations = new Array<ArticleCategory_Locales>();

    Constant.cultures.subscribe(data => data.forEach(value => {
      this.localizations.push(new ArticleCategory_Locales(value.code));
    }));
  }
}

export class ArticleCategory_Locales extends BaseLocalizableEntity_Locale {
  public name: string;
  public cultureCode: string;

  constructor(cultureCode: string) {
    super();
    this.cultureCode = cultureCode;
  }
}
