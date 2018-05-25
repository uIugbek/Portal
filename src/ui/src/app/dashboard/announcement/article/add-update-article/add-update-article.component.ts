import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material';

import { SelectList, Article } from '@core/models';
import { BaseAddUpdateComponent } from '@core/components';
import { FileUploader } from 'ng2-file-upload';
import { Configuration } from 'app/app.configuration';
declare var $: any;

import {
  CityService,
  RegionService,
  ArticleCategoryService,
  ArticleService
} from '@core/services';
import { NewsPreviewComponent } from '../../news/news-preview/news-preview.component';
import { Constant } from 'app/app.constant';
const URL = Constant.server + '/api/File/Upload';

@Component({
  selector: 'app-add-update-article',
  templateUrl: './add-update-article.component.html',
  styleUrls: ['./add-update-article.component.scss'],
  providers: [
    ArticleService,
    RegionService,
    ArticleCategoryService,
    CityService,
  ]
})
export class AddUpdateArticleComponent extends BaseAddUpdateComponent<
Article,
ArticleService
> implements OnInit {
  public regions: SelectList[];
  public cities: SelectList[];
  public articleCategories: SelectList[];
  public cultures = Constant.cultures;

  uploader: FileUploader = new FileUploader({
    url: URL,
    isHTML5: true,
    autoUpload: true
  });

  constructor(
    public location: Location,
    public route: ActivatedRoute,
    public configuration: Configuration,
    public dataService: ArticleService,
    public regionService: RegionService,
    public cityService: CityService,
    public dialog: MatDialog,
    public articleCategoryService: ArticleCategoryService    
  ) {
    super(dataService, location, route);
    this.entity = new Article();
    this.initUploader();
  }

  ngOnInit() {
    super.ngOnInit();
    
    this.articleCategoryService.getManualArticleCategories().subscribe(data => {
      this.articleCategories = data.map(s => {
        let model: SelectList = <SelectList>{
          value: s.id,
          text: s.name,
          selected: false
        };
        return model;
      });
    });

    this.regionService.getAsSelect().subscribe(data => {
      this.regions = data;
    });
  }  

  ngAfterViewInit() {
    Constant.cultures.subscribe(data => data.forEach((element, index) => {
      $("#summernote_" + index).summernote();
    }));
  }

  onGetByIdComplete() {
    super.onGetByIdComplete();
    Constant.cultures.subscribe(data => data.forEach((element, index) => {
      $("#summernote_" + index).summernote('code', this.entity.localizations[index].description);
    }));
  }

  public addOrUpdate() {

    $("img").addClass("img-responsive");
    Constant.cultures.subscribe(data => data.forEach((element, index) => {
      let code = $("#summernote_" + index).summernote('code');
      this.entity.localizations[index].description = code as string;
    }));
    super.addOrUpdate();
  };

  regionChange(region: SelectList) {
    if (region != null) {
      this.cityService.getCitiesByRegion(region.value).subscribe(data => {
        this.cities = data.map(s => {
          let model: SelectList = <SelectList>{
            value: s.id,
            text: s.name,
            selected: false
          };
          return model;
        });
      });
    }
  }

  initUploader() {
    this.uploader.options.additionalParameter = { pathName: 'Photo' };
    this.uploader.onAfterAddingFile = file => {
      file.withCredentials = false;
    };
    this.uploader.onCompleteItem = (
      item: any,
      response: any,
      status: any,
      headers: any
    ) => {
      this.entity.photoPath = JSON.parse(response).fileName;
    };
  }

  openDalog() {
    Constant.cultures.subscribe((cultures) => {
      cultures.forEach((element, index) => {
        let code = $("#summernote_" + index).summernote('code');
        this.entity.localizations[index].description = code as string;
      });
    });
    const dialogRef = this.dialog.open(NewsPreviewComponent, {
      height: '80%',
      width: '70%',
      data: this.entity
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result`);
    });
  }
}
