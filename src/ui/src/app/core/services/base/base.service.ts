import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';

import { AuthService } from "..";

export abstract class BaseService {

  constructor(public authService: AuthService) { }

  public handleError(error: any) {
    var applicationError = error.headers.get('Application-Error');

    if (applicationError) {
      return Observable.throw(applicationError);
    }

    var modelStateErrors: string = '';
    var serverError = error.json();

    if (!serverError.type) {
      for (var key in serverError) {
        if (serverError[key])
          modelStateErrors += serverError[key] + '\n';
      }
    }

    modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
    return Observable.throw(modelStateErrors || 'Server error');
  }

  public getRequestHeaders(): { headers: HttpHeaders | { [header: string]: string | string[]; } } {
    let headers = new HttpHeaders({
      'Authorization': 'Bearer ' + this.authService.accessToken,
      'Content-Type': 'application/json',
      'Accept': `application/vnd.iman.v1.0+json, application/json, text/plain, */*`,
      'App-Version': '1.0'
    });

    return { headers: headers };
  }
}