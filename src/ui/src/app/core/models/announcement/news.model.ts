import {
  BaseLocalizableEntity,
  BaseLocalizableEntity_Locale,
  BaseEntity
} from '../base';
import { Constant } from 'app/app.constant';

export class News extends BaseLocalizableEntity<News_Locales> {
  public name: string;
  public preview: string;
  public description: string;
  public source: string;
  public regionName: string;
  public cityName: string;
  public createdDate: Date;
  public likes: number;
  public views: number;
  public ranking: number;
  public categoryId: number;
  public cityId: number;
  public regionId: number;
  public photoPath: string;
  public categoryName: string;
  constructor() {
    super();
    this.localizations = new Array<News_Locales>();

    Constant.cultures.subscribe(data => data.forEach(value => {
      this.localizations.push(new News_Locales(value.code));
    }));
  }
}
export class News_Locales extends BaseLocalizableEntity_Locale {
  public name: string;
  public description: string;
  public preview: string;
  public source: string;
  public regionName: string;
  public cityName: string;
  constructor(cultureCode: string) {
    super();
    this.cultureCode = cultureCode;
  }
}
