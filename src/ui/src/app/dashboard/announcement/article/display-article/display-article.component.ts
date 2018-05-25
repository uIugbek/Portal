import { Component, OnInit, ViewChild, ElementRef, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { BaseAddUpdateComponent } from '@core/components';

import { Configuration } from 'app/app.configuration';
import { SafeHtml } from '@core/pipes' ;
import { Article } from '@core/models';
import { ArticleService } from '@core/services';

@Component({
  selector: 'app-display',
  templateUrl: './display-article.component.html',
  styleUrls: ['./display-article.component.scss'],
  providers: [ArticleService]
})
export class DisplayArticleComponent extends BaseAddUpdateComponent<
Article,
ArticleService
> implements OnInit {
public info: any;
constructor(
  public location: Location,
  public route: ActivatedRoute,
  public configuration: Configuration,
  public dataService: ArticleService,  
) {
  super(dataService, location, route);
  this.entity = new Article(); 
}

ngOnInit() {
  super.ngOnInit();   
  this.info = this.entity.description;  
  }
}
