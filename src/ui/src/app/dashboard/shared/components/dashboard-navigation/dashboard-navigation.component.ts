import { Component, OnInit } from '@angular/core';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';

import { Configuration } from 'app/app.configuration';
import { Culture } from '@core/models';
import { AuthService } from "@core/services/account/auth.service";
import { Constant } from 'app/app.constant';

@Component({
  selector: 'dashboard-navigation',
  templateUrl: './dashboard-navigation.component.html',
  styleUrls: ['./dashboard-navigation.component.scss']
})
export class DashboardNavigationComponent implements OnInit {

  selected = localStorage.getItem('lang') 
    ? JSON.parse(localStorage.getItem('lang')) as Culture 
    : new Culture(0, Constant.defaultLangName, Constant.defaultLang, Constant.defaultLangIcon);
  cultures = Constant.cultures;
  
  constructor(
    public translate: TranslateService,
    public configuration: Configuration,
    public userService: AuthService
  ) { 
  }

  logout() {
    this.userService.logout();
  }

  ngOnInit() {
    
  }

  changeLang(language: Culture): void {
    this.translate.use(language.code);
    this.selected = language;
    localStorage.setItem(Constant.LANG_KEY, JSON.stringify(language));
  }

}
