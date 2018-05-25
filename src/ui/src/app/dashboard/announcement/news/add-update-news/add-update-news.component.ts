import {
  Component, OnInit,
  Input,
} from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material';
import { MatExpansionModule } from '@angular/material/expansion';
import { FileUploader } from 'ng2-file-upload';

import { BaseAddUpdateComponent } from '@core/components';
import { SelectList, Culture, News } from '@core/models';
import { NewsPreviewComponent } from '../news-preview/news-preview.component';
import { Configuration } from 'app/app.configuration';
declare var $: any;

import {
  CityService,
  NewsService,
  NewsCategoryService,
  RegionService
} from '@core/services';
import { ArticlePreviewComponent } from 'app/dashboard/announcement/article/article-preview/article-preview.component';
import { Constant } from '../../../../app.constant';
declare var $: any;
const URL = Constant.server + '/api/File/Upload';

@Component({
  selector: 'app-add-update-news',
  templateUrl: './add-update-news.component.html',
  styleUrls: ['./add-update-news.component.scss'],
  providers: [
    NewsService,
    RegionService,
    CityService,
    NewsCategoryService]
})
export class AddUpdateNewsComponent extends BaseAddUpdateComponent<
News,
NewsService
> implements OnInit {
  public regions: SelectList[];
  public cities: SelectList[];
  public newsCategories: SelectList[];
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
    public dataService: NewsService,
    public regionService: RegionService,
    public cityService: CityService,
    public dialog: MatDialog,
    public newsCategoryService: NewsCategoryService
  ) {
    super(dataService, location, route);
    this.entity = new News();   
    this.initUploader(); 
  }

  ngOnInit() {
    super.ngOnInit();

    this.newsCategoryService.getAsSelect().subscribe(data => {
      this.newsCategories = data;
    });

    this.regionService.getAsSelect().subscribe(data => {
      this.regions = data;
    });
    this.cityService.getAsSelect().subscribe(data => {
      this.cities = data;
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

  openDalog() {
    Constant.cultures.subscribe((cultures) => {
      cultures.forEach((element, index) => {
        let code = $("#summernote_" + index).summernote('code');
        this.entity.localizations[index].description = code as string;
      });
    });
    const dialogRef = this.dialog.open(ArticlePreviewComponent, {
      height: '80%',
      width: '70%',
      data: this.entity
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result`);
    });
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
      console.log(this.entity.photoPath);
    };
  }
}
