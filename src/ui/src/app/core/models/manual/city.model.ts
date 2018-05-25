import { BaseLocalizableEntity, BaseLocalizableEntity_Locale } from '../base';
import { Constant } from 'app/app.constant';
import {
  News,
  NewsCategory,
  Article
} from 'app/core';

export class City extends BaseLocalizableEntity<City_Locales> {
  public name: string;
  public preview: string;
  public description: string;
  public population: number;
  public code: string;
  public likes: number;
  public ranking: number;
  public views: number;
  public regionId: number;
  public previewPhotoPath: string;
  public newsCategories: NewsCategory[];
  public news: News[];
  public articles: Article[];
  
  constructor() {
    super();
    this.localizations = new Array<City_Locales>();

    Constant.cultures.subscribe(data => data.forEach(value => {
      this.localizations.push(new City_Locales(value.code));
    }));
  }
}
export class City_Locales extends BaseLocalizableEntity_Locale {
  public name: string;
  public description: string;
  public preview: string;
  constructor(cultureCode: string) {
    super();
    this.cultureCode = cultureCode;
  }
}
