import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

import { BaseAddUpdateComponent } from '@core/components';
import { CultureService } from '@core/services';
import { Culture, FileModel, BaseLocalizableEntity_Locale } from '@core/models';
import { Configuration } from 'app/app.configuration';
import { Constant } from 'app/app.constant';

@Component({
  selector: 'app-add-update-culture',
  templateUrl: './add-update-culture.component.html',
  styleUrls: ['./add-update-culture.component.scss'],
  providers: [CultureService]
})
export class AddUpdateCultureComponent extends BaseAddUpdateComponent<
Culture,
CultureService
> implements OnInit {

  cultures = Constant.cultures;
  
  constructor(
    public dataService: CultureService,
    public location: Location,
    public route: ActivatedRoute,
    public configuration: Configuration
  ) {
    super(dataService, location, route);
    this.entity = new Culture();
  }

  ngOnInit() {
    super.ngOnInit();
  }

  onAdd() {
    super.onAdd();
    this.configuration.reLoadCultures();
  }

  onUpdate() {
    super.onUpdate();
    this.configuration.reLoadCultures();
  }

  onFileChange(event) {
    let reader = new FileReader();
    if (event.target.files && event.target.files.length > 0) {
      let file = event.target.files[0];
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.entity.file = new FileModel();
        this.entity.file.fileName = file.name,
          this.entity.file.fileType = file.type,
          this.entity.file.value = reader.result.split(',')[1]
      };
    }
  }
}
