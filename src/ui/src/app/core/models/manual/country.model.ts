import { Constant } from 'app/app.constant';
import {
  BaseLocalizableEntity,
  BaseLocalizableEntity_Locale
} from '../base';

export class Country extends BaseLocalizableEntity<Country_Locales> {
  public code: string;
  public name: string;
  constructor(
    id?: number,
    name?: string,
    code?: string,
    localizations?: Array<Country_Locales>
  ) {
    super();
    this.id = id;
    this.name = name;
    this.code = code;
    if (localizations != null) {
      this.localizations = localizations;
    } else {
      this.localizations = new Array<Country_Locales>();

      Constant.cultures.subscribe(data => data.forEach(value => {
        this.localizations.push(new Country_Locales(value.code));
      }));
    }
  }
}
export class Country_Locales extends BaseLocalizableEntity_Locale {
  public name: string;
  constructor(cultureCode: string) {
    super();
    this.cultureCode = cultureCode;
  }
}
