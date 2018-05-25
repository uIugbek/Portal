import {
  BaseLocalizableEntity,
  BaseLocalizableEntity_Locale,
  BaseEntity
} from '../base';
import { Constant } from 'app/app.constant';

export class Article extends BaseLocalizableEntity<Article_Locales> {
  public name: string;
  public preview: string;
  public description: string;
  public source: string;
  public photoPath: string;
  public likes: number;
  public views: number;
  public ranking: number;
  public articleCategoryId: number;
  public cityId: number;
  public regionId: number;
  public regionName: string;
  public cityName: string;
  public createdDate: Date;
  public categoryName: string;
  constructor() {
    super();
    this.localizations = new Array<Article_Locales>();

    Constant.cultures.subscribe(data => data.forEach(value => {
      this.localizations.push(new Article_Locales(value.code));
    }));
  }
}
export class Article_Locales extends BaseLocalizableEntity_Locale {
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
