import { Injectable, Inject, Optional } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { Constant } from 'app/app.constant';
import { BaseEntity, SelectList } from '../../models';
import { AuthService } from '@core/services/account';

@Injectable()
export class BaseEntityService<TEntity extends BaseEntity>
  implements IBaseEntityService<TEntity> {
  public baseUrl: string;

  constructor(
    public http: HttpClient,
    public translate: TranslateService,
    public authService: AuthService,
    @Inject('actionUrl') @Optional() public actionUrl: string
  ) {
    this.baseUrl = Constant.server + '/api' + '/';
  }

  getAllAsDataResult(state: any): Observable<GridDataResult> {
    var filter = JSON.stringify(state);
    return this.http.get<GridDataResult>(
      this.baseUrl + this.translate.currentLang + this.actionUrl,
      {
        headers: this.getRequestHeaders(),
        params: { filter }
      }
    );
  }

  getAll(): Observable<TEntity[]> {
    return this.http.get<TEntity[]>(
      this.baseUrl + this.translate.currentLang + this.actionUrl,
      {
        headers: this.getRequestHeaders()
      }
    );
  }

  getById(id: any): Observable<TEntity> {
    return this.http.get<TEntity>(
      this.baseUrl + this.translate.currentLang + this.actionUrl + '/' + id,
      {
        headers: this.getRequestHeaders()
      }
    );
  }
 
  add(model: TEntity): Observable<TEntity> {
    return this.http.post<TEntity>(
      this.baseUrl + this.translate.currentLang + this.actionUrl,
      model,
      {
        headers: this.getRequestHeaders(),
      }
    );
  }

  deleteById(id: string | number) {
    return this.http.delete(
      this.baseUrl + this.translate.currentLang + this.actionUrl + '/' + id,
      {
        headers: this.getRequestHeaders()
      }
    );
  }

  update(id: any, model: TEntity) {
    return this.http.put(
      this.baseUrl + this.translate.currentLang + this.actionUrl,
      model,
      {
        headers: this.getRequestHeaders()
      }
    );
  }

  getAsSelect(): Observable<SelectList[]> {
    return this.http.get<SelectList[]>(
      this.baseUrl +
      this.translate.currentLang +
      this.actionUrl +
      '/GetAsSelect',
      {
        headers: this.getRequestHeaders()
      }
    );
  }

  // uploadFile(file: File, fileName: string, filePath: string) {
  //   let formData: FormData = new FormData();
  //   let headers = new HttpHeaders()
  //   formData.append(filePath, file, fileName);
  //   headers.append('Content-Type', 'application/json');
  //   headers.append('Accept', 'application/json');
  //   this.http.post(
  //     this.baseUrl + 'FileUploader/Upload',
  //     formData,
  //     {
  //       headers: headers
  //     }
  //   )
  //     .subscribe(
  //       data => console.log('success'),
  //       error => console.log(error)
  //     )
  // }

  public getRequestHeaders(): HttpHeaders {
    let headers = new HttpHeaders({
      Authorization: 'Bearer ' + this.authService.accessToken,
      'Content-Type': 'application/json',
      Accept: `application/vnd.iman.v1.0+json, application/json, text/plain, */*`,
      'App-Version': '1.0'
    });

    return headers;
  }
}

export interface IBaseEntityService<TEntity> {
  getAll(): Observable<TEntity[]>;
  getById(id: number | any): Observable<TEntity>;
  getAllAsDataResult(state: any): Observable<GridDataResult>;
  add(model: TEntity): Observable<TEntity>;
  deleteById(id: number | string);
  update(id: number | string, model: TEntity);
  // uploadFile(file: File, fileName: string, filePath: string): void;
  getAsSelect(): Observable<SelectList[]>;
}
