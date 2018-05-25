import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

import { BaseAddUpdateComponent } from '@core/components';
import { LanguageService } from '@core/services';
import { Language } from '@core/models';
import { Configuration } from 'app/app.configuration';
import { Constant } from 'app/app.constant';

@Component({
  selector: 'app-add-update-language',
  templateUrl: './add-update-language.component.html',
  styleUrls: ['./add-update-language.component.scss'],
  providers: [LanguageService]
})
export class AddUpdateLanguageComponent extends BaseAddUpdateComponent<
Language,
LanguageService
> implements OnInit {
  public cultures = Constant.cultures;
  constructor(
    public dataService: LanguageService,
    public location: Location,
    public route: ActivatedRoute,
    public configuration: Configuration
  ) {
    super(dataService, location, route);
    this.entity = new Language();
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
}
