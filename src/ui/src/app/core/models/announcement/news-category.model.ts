import {
  BaseLocalizableEntity,
  BaseLocalizableEntity_Locale,
  BaseEntity
} from '../base';
import { Constant } from 'app/app.constant';
export class NewsCategory extends BaseLocalizableEntity<NewsCategory_Locales> {
  public code: string;
  public name: string;
  public order: number;
  public parentId: number;
  public cityId: number;
  public regionId: number;

  public localizations: Array<NewsCategory_Locales> = [];

  constructor() {
    super();

    this.localizations = new Array<NewsCategory_Locales>();

    Constant.cultures.subscribe(data => data.forEach(value => {
      this.localizations.push(new NewsCategory_Locales(value.code));
    }));
  }
}

export class NewsCategory_Locales extends BaseLocalizableEntity_Locale {
  public name: string;
  constructor(cultureCode: string) {
    super();
    this.cultureCode = cultureCode;
  }
}
