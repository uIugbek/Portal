import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { User, FileModel } from '@core/models';
import { UserService, RoleService, CountryService } from '@core/services';
import { BaseAddUpdateComponent } from '@core/components';
import { SelectList } from '@core/models';
import { Constant } from 'app/app.constant';

@Component({
  templateUrl: './add-update-user.component.html',
  styleUrls: ['./add-update-user.component.scss'],
  providers: [UserService, RoleService, CountryService]
})
export class AddUpdateUserComponent extends BaseAddUpdateComponent<
User,
UserService
> implements OnInit {

  roles: string[];
  ages: SelectList[];
  countries: SelectList[];
  cultures = Constant.cultures;

  constructor(
    public route: ActivatedRoute,
    public dataService: UserService,
    public roleService: RoleService,
    public countryService: CountryService,
    public location: Location
  ) {
    super(dataService, location, route);
  }

  ngOnInit() {
    this.entity = new User();
    super.ngOnInit();

    this.getAges();
    this.getRoles();
    this.getCountries();

  }

  getRoles() {
    this.roleService.getAllNames().subscribe(
      data => { this.roles = data; },
      error => { console.log(error); }
    )
  }

  getAges() {
    this.dataService.getAges().subscribe(
      data => { this.ages = data; },
      error => { console.log(error); }
    )
  }

  getCountries() {
    this.countryService.getAsSelect().subscribe(
      data => { this.countries = data; },
      error => { console.log(error); }
    )
  }

  onFileChange(event) {
    let reader = new FileReader();
    if (event.target.files && event.target.files.length > 0) {
      let file = event.target.files[0];
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.entity.avatar = new FileModel();

        this.entity.avatar.fileName = file.name,
        this.entity.avatar.fileType = file.type,
        this.entity.avatar.value = reader.result.split(',')[1]
      };
    }
  }
}
