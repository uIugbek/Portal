import { Injectable } from '@angular/core';
import { Router, NavigationExtras } from "@angular/router";
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { UserRegistration } from '../../models/account';
import { Constant } from "app/app.constant";

import { Observable } from 'rxjs/Rx';
import { Subject } from 'rxjs/Subject';
import { BehaviorSubject } from 'rxjs/Rx';
import '../../../rxjs-operators';

import { LocalStoreManager } from "../base/local-store-manager.service";
import { User, Permission } from '../../models';

@Injectable()
export class AuthService {

  public get loginUrl() { return Constant.loginUrl; }

  public loginRedirectUrl: string;
  public logoutRedirectUrl: string;

  public reLoginDelegate: () => void;

  public previousIsLoggedInCheck = false;
  public _loginStatus = new Subject<boolean>();
  public _authNavStatusSource = new BehaviorSubject<boolean>(false);
  public loggedIn = false;

  baseUrl: string = '';
  authNavStatus$ = this._authNavStatusSource.asObservable();

  constructor(
    public http: Http,
    public localStorageManager: LocalStoreManager
  ) {
    this.loggedIn = !!localStorage.getItem('auth_token');
    this._authNavStatusSource.next(this.loggedIn);
    this.baseUrl = Constant.server + '/api/auth';
    this.initializeLoginStatus();
  }

  register(email: string, password: string, firstName: string, lastName: string, location: string): Observable<UserRegistration> {
    let body = JSON.stringify({ email, password, firstName, lastName, location });
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    return this.http.post(this.baseUrl + "/register", body, options)
      .map(res => true)
      .catch(this.handleError);
  }

  login(userName, password) {

    if (this.isLoggedIn)
      this.logout();

    let headers = new Headers();
    headers.append('Content-Type', 'application/json');

    return this.http
      .post(
        this.baseUrl + '/login',
        JSON.stringify({ userName, password }), { headers }
      )
      .map(res => res.json())
      .map(res => {
        localStorage.setItem(Constant.AUTH_TOKEN, res.auth_token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        return true;
      })
      .catch(this.handleError);
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }

  isLoggedIn() {
    return this.loggedIn;
  }

  facebookLogin(accessToken: string) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let body = JSON.stringify({ accessToken });
    return this.http
      .post(
        this.baseUrl + '/facebook', body, { headers })
      .map(res => res.json())
      .map(res => {
        localStorage.setItem(Constant.AUTH_TOKEN, res.auth_token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        return true;
      })
      .catch(this.handleError);
  }

  public reevaluateLoginStatus(currentUser?: User) {

    let user = currentUser || this.localStorageManager.getDataObject<User>(Constant.CURRENT_USER);
    let isLoggedIn = user != null;

    if (this.previousIsLoggedInCheck != isLoggedIn) {
      setTimeout(() => {
        this._loginStatus.next(isLoggedIn);
      });
    }

    this.previousIsLoggedInCheck = isLoggedIn;
  }

  public initializeLoginStatus() {
    this.localStorageManager.getInitEvent().subscribe(() => {
      this.reevaluateLoginStatus();
    });
  }

  get userPermissions(): Permission[] {
    return this.localStorageManager.getDataObject<Permission[]>(Constant.USER_PERMISSIONS) || [];
  }

  get accessToken(): string {

    this.reevaluateLoginStatus();
    return this.localStorageManager.getData(Constant.AUTH_TOKEN);
  }

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

}

