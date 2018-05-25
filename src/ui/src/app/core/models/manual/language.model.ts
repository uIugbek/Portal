import { Constant } from 'app/app.constant';
import {
  BaseLocalizableEntity,
  BaseLocalizableEntity_Locale
} from '../base';

export class Language extends BaseLocalizableEntity<Language_Locales> {
  public code: string;
  public name: string;
  constructor(
    id?: number,
    name?: string,
    code?: string,
    localizations?: Array<Language_Locales>
  ) {
    super();
    this.id = id;
    this.name = name;
    this.code = code;
    if (localizations != null) {
      this.localizations = localizations;
    } else {
      this.localizations = new Array<Language_Locales>();

      Constant.cultures.subscribe(data => data.forEach(value => {
        this.localizations.push(new Language_Locales(value.code));
      }));
    }
  }
}
export class Language_Locales extends BaseLocalizableEntity_Locale {
  public name: string;
  constructor(cultureCode: string) {
    super();
    this.cultureCode = cultureCode;
  }
}
