import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth.guard';

import { DashboardComponent } from './dashboard.component';

import {

  /****** user-management ******/
  UserListComponent,
  AddUpdateUserComponent,

  RoleListComponent,
  AddUpdateRoleComponent,

  /****** manual ******/
  CultureListComponent,
  AddUpdateCultureComponent,

  CityListComponent,
  AddUpdateCityComponent,

  RegionListComponent,
  AddUpdateRegionComponent,

  CountryListComponent,
  AddUpdateCountryComponent,

  LanguageListComponent,
  AddUpdateLanguageComponent,

  /****** announcement ******/
  ArticleCategoryListComponent,
  AddUpdateArticleCategoryComponent,

  ArticleListComponent,
  AddUpdateArticleComponent,

  DisplayNewsComponent,
  DisplayArticleComponent,

  NewsCategoryListComponent,
  AddUpdateNewsCategoryComponent,

  NewsListComponent,
  AddUpdateNewsComponent,

  /****** others ******/
  FisheyeComponent
} from '.';

const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard],
    children: [

      /****** user-management ******/
      { path: 'user', component: UserListComponent },
      { path: 'user/add', component: AddUpdateUserComponent },
      { path: 'user/update/:id', component: AddUpdateUserComponent },

      { path: 'role', component: RoleListComponent },
      { path: 'role/add', component: AddUpdateRoleComponent },
      { path: 'role/update/:id', component: AddUpdateRoleComponent },

      /****** manual ******/
      { path: 'culture', component: CultureListComponent },
      { path: 'culture/add', component: AddUpdateCultureComponent },
      { path: 'culture/update/:id', component: AddUpdateCultureComponent },

      { path: 'city', component: CityListComponent },
      { path: 'city/add/:regionId', component: AddUpdateCityComponent },
      { path: 'city/update/:id', component: AddUpdateCityComponent },

      { path: 'region', component: RegionListComponent },
      { path: 'region/add/:countryId', component: AddUpdateRegionComponent },
      { path: 'region/update/:id', component: AddUpdateRegionComponent },

      { path: 'country', component: CountryListComponent },
      { path: 'country/add', component: AddUpdateCountryComponent },
      { path: 'country/update/:id', component: AddUpdateCountryComponent },

      { path: 'language', component: LanguageListComponent },
      { path: 'language/add', component: AddUpdateLanguageComponent },
      { path: 'language/update/:id', component: AddUpdateLanguageComponent },

      /****** announcement ******/
      { path: 'articleCategory', component: ArticleCategoryListComponent },
      { path: 'articleCategory/add', component: AddUpdateArticleCategoryComponent },
      { path: 'articleCategory/update/:id', component: AddUpdateArticleCategoryComponent },

      { path: 'article', component: ArticleListComponent },
      { path: 'article/add', component: AddUpdateArticleComponent },
      { path: 'article/update/:id', component: AddUpdateArticleComponent },
      { path: 'article/display/:id', component: DisplayArticleComponent },

      { path: 'news', component: NewsListComponent },
      { path: 'news/add', component: AddUpdateNewsComponent },
      { path: 'news/update/:id', component: AddUpdateNewsComponent },
      { path: 'news/display/:id', component: DisplayNewsComponent },

      { path: 'newsCategory', component: NewsCategoryListComponent },
      { path: 'newsCategory/add', component: AddUpdateNewsCategoryComponent },
      { path: 'newsCategory/update/:id', component: AddUpdateNewsCategoryComponent },

      /****** others ******/
      { path: 'fisheye', component: FisheyeComponent }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class DashboardRoutingModule { }
