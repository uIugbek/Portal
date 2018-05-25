import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

import { Role } from '@core/models';
import { RoleService } from '@core/services';
import { BaseKendoGridComponent } from '@core/components';
import { TranslateService } from '@ngx-translate/core';

@Component({
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss'],
  providers: [RoleService]
})
export class RoleListComponent extends BaseKendoGridComponent<
  Role,
  RoleService
> implements OnInit {
  constructor(
    public dataService: RoleService,
    public translate: TranslateService
  ) {
    super(dataService, translate);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
