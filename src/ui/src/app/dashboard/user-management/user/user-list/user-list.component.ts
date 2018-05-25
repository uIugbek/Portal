import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { BaseKendoGridComponent } from '@core/components';
import { UserService } from '@core/services';
import { User } from '@core/models';

@Component({
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
  providers: [UserService]
})
export class UserListComponent extends BaseKendoGridComponent<
  User,
  UserService
> implements OnInit {
  constructor(
    public dataService: UserService,
    public translate: TranslateService
  ) {
    super(dataService, translate);
  }

  ngOnInit() {
    super.ngOnInit();
  }
}
