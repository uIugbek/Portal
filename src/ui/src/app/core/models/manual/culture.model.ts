import { Constant } from 'app/app.constant';
import {
  BaseLocalizableEntity,
  BaseLocalizableEntity_Locale
} from '../base';
import { FileModel } from "../file.model";

export class Culture extends BaseLocalizableEntity<Culture_Locales> {
  public code: string;
  public name: string;
  public icon: string;
  public file: FileModel;
  
  constructor(
    id?: number,
    name?: string,
    code?: string,
    icon?: string,
    localizations?: Array<Culture_Locales>
  ) {
    super();
    this.id = id;
    this.name = name;
    this.code = code;
    this.icon = icon;

    if (localizations != null) {
      this.localizations = localizations;
    } else {
      this.localizations = new Array<Culture_Locales>();

      Constant.cultures.subscribe(data => data.forEach(value => {
        this.localizations.push(new Culture_Locales(value.code));
      }));
    }
  }
}
export class Culture_Locales extends BaseLocalizableEntity_Locale {
  public name: string;
  constructor(cultureCode: string) {
    super();
    this.cultureCode = cultureCode;
  }
}
