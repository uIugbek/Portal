export class Permission {
  public static readonly USER_CRUD = 'USER_CRUD';
  public static readonly ROLE_CRUD = 'ROLE_CRUD';

  public static readonly NEWS_CATEGORY_CRUD = "NEWS_CATEGORY_CRUD";
  public static readonly NEWS_CRUD = "NEWS_CRUD";
  public static readonly ARTICLE_CATEGORY_CRUD = "ARTICLE_CATEGORY_CRUD";
  public static readonly ARTICLE_CRUD = "ARTICLE_CRUD";

  public static readonly COUNTRY_CRUD = 'COUNTRY_CRUD';
  public static readonly REGION_CRUD = 'REGION_CRUD';
  public static readonly CITY_CRUD = 'CITY_CRUD';

  public static getAll(): string[] {
    return [
      this.USER_CRUD,
      this.ROLE_CRUD,

      this.NEWS_CATEGORY_CRUD,
      this.NEWS_CRUD,
      this.ARTICLE_CATEGORY_CRUD,
      this.ARTICLE_CRUD,
      
      this.COUNTRY_CRUD,
      this.REGION_CRUD,
      this.CITY_CRUD
    ];
  }
  constructor() {}
}
