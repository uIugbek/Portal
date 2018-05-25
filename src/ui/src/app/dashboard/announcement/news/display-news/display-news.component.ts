import { Component, OnInit, ViewChild, ElementRef, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { SelectList, Culture, News } from '@core/models';
import { BaseAddUpdateComponent } from '@core/components';
import { Configuration } from 'app/app.configuration';
import { SafeHtml } from '@core/pipes' ;
import { NewsService } from '@core/services/announcement';

@Component({
  selector: 'app-display',
  templateUrl: './display-news.component.html',
  styleUrls: ['./display-news.component.scss'],
  providers: [NewsService],
})
export class DisplayNewsComponent extends BaseAddUpdateComponent<
News,
NewsService
> implements OnInit {
public info: any;
constructor(
  public location: Location,
  public route: ActivatedRoute,
  public configuration: Configuration,
  public dataService: NewsService,  
) {
  super(dataService, location, route);
  this.entity = new News(); 
}

ngOnInit() {
  super.ngOnInit();   
  this.info = this.entity.description;  
  }
}
