import { Culture } from '@core/models';
import { Observable } from 'rxjs/Observable';
import { MIMEConstant } from './core/enums/mime.constant';

export class Constant {
  public static server = 'http://localhost:5055';

  public static host = 'http://localhost:4205';

  public static loginUrl: string = '/login';

  public static cultures: Observable<Culture[]>;

  public static readonly defaultLang = 'en';
  
  public static readonly defaultLangIcon = Constant.server + '/images/en.png';

  public static readonly defaultLangName = 'English';

  public static readonly gridPaginationSize = 5;

  public static readonly CURRENT_USER = 'current_user';

  public static readonly USER_PERMISSIONS = 'user_permissions';

  public static readonly AUTH_TOKEN = 'auth_token';

  public static readonly TOKEN_EXPIRES_IN = 'expires_in';

  public static readonly imageAllowedTypes = ['jpeg', 'jpg', 'png'];

  public static readonly imageMaxSize = 5 * 1024 * 1024;

  public static readonly fileAllowedTypes = [
    MIMEConstant.TextPlain,
    MIMEConstant.ApplicationMsword,
    MIMEConstant.ApplicationPdf,
    MIMEConstant.ApplicationJson,
    MIMEConstant.TextHtml,
    MIMEConstant.ApplicationVndOpenxmlformatsOfficedocumentPresentationmlPresentation,
    MIMEConstant.ApplicationVndMsExcel,
    MIMEConstant.ApplicationVndOasisOpendocumentSpreadsheet,
    MIMEConstant.ApplicationVndMsPowerpoint,
    MIMEConstant.ApplicationVndOpenxmlformatsOfficedocumentWordprocessingmlDocument
  ];

  public static readonly fileMaxSize = 1024 * 1024 * 10;

  // LocalStorage keys
  public static readonly LANG_KEY = 'lang';
  public static readonly LANGS_KEY = 'langs';

  public static currentLang(): string {
    let hasLang =
      localStorage.getItem(Constant.LANG_KEY) === '' ||
      localStorage.getItem(Constant.LANG_KEY) === undefined ||
      localStorage.getItem(Constant.LANG_KEY) === null;
      
    let currentLang = hasLang
      ? Constant.defaultLang
      : (JSON.parse(localStorage.getItem(Constant.LANG_KEY)) as Culture).code;

    return currentLang;
  }
}
