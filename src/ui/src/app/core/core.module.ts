import { NgModule, ModuleWithProviders, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import {
  TranslateModule,
  TranslateLoader,
  TranslateService
} from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AngularOpenlayersModule } from 'ngx-openlayers';
import { InfiniteScrollModule } from 'angular2-infinite-scroll';

import { MaterialModule } from "./material/material.module";

import { Configuration } from '../app.configuration';
import { Constant } from '../app.constant';
import { MyFocus } from './directives/focus.directive';
import { SafeHtml, SafeUrl } from './pipes';

import { SpinnerComponent } from "./components";

import { AuthService, LocalStoreManager } from "./services";
import { OnCreate } from '@core/directives/on-create.directive';


export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, Constant.server + '/i18n/', '.json');
}
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    MaterialModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    TranslateModule.forChild({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    AngularOpenlayersModule,
    InfiniteScrollModule,
  ],

  declarations: [
    MyFocus,
    /****** pipes ******/
    SafeHtml,
    SafeUrl,
    SpinnerComponent,
    OnCreate
  ],

  exports: [
    MyFocus,
    SpinnerComponent,
    TranslateModule,
    MaterialModule,
    AngularOpenlayersModule,
    InfiniteScrollModule,
    SafeHtml,
    SafeUrl,
    OnCreate
  ],

  providers: [
    Configuration,
    AuthService,
    LocalStoreManager,
    { provide: 'actionUrl', useValue: '' }
  ],
})
export class CoreModule {

  constructor(public configuration: Configuration) {
    configuration.load();
  }
}
