import { Constant } from 'app/app.constant';

export interface IEntity<TKey> {
  id: TKey;
}

export class Entity<TKey> implements IEntity<TKey> {
  id: TKey;
}

export class BaseEntity extends Entity<number> {

  constructor() {
    super();
  }
}

export class BaseLocalizableEntity<TEntity_Locale extends BaseLocalizableEntity_Locale> extends BaseEntity {

  localizations: Array<TEntity_Locale> = [];

  constructor() {
    super();
  }
}

export class BaseLocalizableEntity_Locale extends BaseEntity {

  cultureCode: string;

  constructor() {
    super();
  }

}