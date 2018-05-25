import {
  BaseLocalizableEntity,
  BaseLocalizableEntity_Locale
} from '../base';
import { Constant } from 'app/app.constant';

export class Region extends BaseLocalizableEntity<Region_Locales> {
  public name: string;
  public preview: string;
  public description: string;
  public population: number;
  public code: string;
  public views: number;
  public countryId: number;
  public previewPhotoPath: string;
  
  constructor() {
    super();
    this.localizations = new Array<Region_Locales>();

    Constant.cultures.subscribe(data => data.forEach(value => {
      this.localizations.push(new Region_Locales(value.code));
    }));
  }
}

export class Region_Locales extends BaseLocalizableEntity_Locale {
  public name: string;
  public preview: string;
  public description: string;
  public cultureCode: string;

  constructor(cultureCode: string) {
    super();
    this.cultureCode = cultureCode;
  }
}
