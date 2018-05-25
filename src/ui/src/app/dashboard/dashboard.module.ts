import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { GridModule } from '@progress/kendo-angular-grid';
import { PopupModule } from '@progress/kendo-angular-popup';
import { SortableModule } from '@progress/kendo-angular-sortable';

import { NgxMaskModule } from 'ngx-mask';
import { FileUploadModule } from 'ng2-file-upload';
import { InfiniteScrollModule } from 'angular2-infinite-scroll';

import { CoreModule } from '../core/core.module';
import { DashboardRoutingModule } from './dashboard-routing.module';

import { DashboardComponent } from './dashboard.component';
import { DashboardNavigationComponent } from './shared/components';

import {
  UserListComponent,
  AddUpdateUserComponent,

  RoleListComponent,
  AddUpdateRoleComponent,

  CultureListComponent,
  AddUpdateCultureComponent,

  CountryListComponent,
  AddUpdateCountryComponent,

  RegionListComponent,
  AddUpdateRegionComponent,

  CityListComponent,
  AddUpdateCityComponent,

  LanguageListComponent,
  AddUpdateLanguageComponent,

  ArticleCategoryListComponent,
  AddUpdateArticleCategoryComponent,

  ArticleListComponent,
  DisplayArticleComponent,
  AddUpdateArticleComponent,

  NewsListComponent,
  DisplayNewsComponent,
  AddUpdateNewsComponent,
  NewsPreviewComponent,

  NewsCategoryListComponent,
  AddUpdateNewsCategoryComponent,

  FisheyeComponent,
  FisheyeDialogComponent
} from '.';

import { DataService } from '@core/services';

import { PDFExportModule } from '@progress/kendo-angular-pdf-export';
import { ArticlePreviewComponent } from './announcement/article/article-preview/article-preview.component';
import { ToastrModule, ToastContainerModule } from 'ngx-toastr';

@NgModule({
  imports: [

    /****** angular ******/
    FormsModule,
    CommonModule,
    ReactiveFormsModule,

    /****** kendo ******/
    GridModule,
    PopupModule,
    SortableModule,
    NgxMaskModule.forRoot(),
    PDFExportModule,

    /****** others ******/
    FileUploadModule,
    InfiniteScrollModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
    }),

    /****** self ******/
    CoreModule,
    DashboardRoutingModule
  ],

  declarations: [
    DashboardComponent,
    DashboardNavigationComponent,

    /****** user-management ******/
    UserListComponent,
    AddUpdateUserComponent,

    RoleListComponent,
    AddUpdateRoleComponent,

    /****** manual ******/
    CultureListComponent,
    AddUpdateCultureComponent,

    RegionListComponent,
    AddUpdateRegionComponent,

    CountryListComponent,
    AddUpdateCountryComponent,

    CityListComponent,
    AddUpdateCityComponent,

    LanguageListComponent,
    AddUpdateLanguageComponent,

    /****** announcement ******/
    ArticleCategoryListComponent,
    AddUpdateArticleCategoryComponent,

    ArticleListComponent,
    DisplayArticleComponent,
    AddUpdateArticleComponent,

    NewsCategoryListComponent,
    AddUpdateNewsCategoryComponent,

    NewsListComponent,
    NewsPreviewComponent,
    DisplayNewsComponent,
    AddUpdateNewsComponent,

    /****** others ******/
    FisheyeComponent,
    FisheyeDialogComponent,

    ArticlePreviewComponent,

  ],

  exports: [
    
  ],

  providers: [
    DataService
  ],

  entryComponents: [
    FisheyeDialogComponent,
    NewsPreviewComponent,
    ArticlePreviewComponent
  ]
})
export class DashboardModule { }
