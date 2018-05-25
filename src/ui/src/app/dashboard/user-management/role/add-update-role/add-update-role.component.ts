import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { State } from '@progress/kendo-data-query';
import { Observable } from 'rxjs/Observable';

import { BaseAddUpdateComponent } from '@core/components';
import { Constant } from 'app/app.constant';
import { Configuration } from 'app/app.configuration';
import { Permission, Role } from "@core/models";
import { RoleService } from '@core/services';

@Component({
  templateUrl: './add-update-role.component.html',
  styleUrls: ['./add-update-role.component.scss'],
  providers: [RoleService]
})
export class AddUpdateRoleComponent extends BaseAddUpdateComponent<
  Role,
  RoleService
> implements OnInit {

  public permissions: string[] = Permission.getAll();
  public cultures = Constant.cultures;

  constructor(
    public route: ActivatedRoute,
    public dataService: RoleService,
    public location: Location,
    public configuration: Configuration
  ) {
    super(dataService, location, route);
  }

  ngOnInit() {
    super.ngOnInit();
    this.entity = new Role();
  }
}
