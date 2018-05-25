import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Culture } from '@core/models';

import { Configuration } from 'app/app.configuration';
import { Constant } from 'app/app.constant';
import * as ol from 'openlayers';

@Component({
  selector: 'app-article-preview',
  templateUrl: './article-preview.component.html',
  styleUrls: ['./article-preview.component.scss']
})
export class ArticlePreviewComponent implements OnInit {

  map: ol.Map;
  selected = {
    id: 0,
    code: Constant.defaultLang,
    name: Constant.defaultLangName,
    icon: Constant.defaultLangIcon,
    index: 0
  };
  cultures: any;

  projection = ol.proj.get('EPSG:3857');

  raster = new ol.layer.Tile({
    source: new ol.source.OSM()
  });

  vector = new ol.layer.Vector({
    source: new ol.source.Vector({
      url: Constant.server + '/Storage/KmlFiles/' + this.data.kml,
      format: new ol.format.KML(),
    })
  });

  constructor(
    public dialogRef: MatDialogRef<ArticlePreviewComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public configuration: Configuration
  ) {
    console.log(data);
    Constant.cultures.subscribe(ee => {
      let i = 0;
      this.cultures = ee.map(s => {
        const cl = {
          id: s.id,
          code: s.code,
          name: s.name,
          index: i++
        }
        return cl;
      })
    })
  }

  ngOnInit() {
    this.map = new ol.Map({
      layers: [this.raster, this.vector],
      target: document.getElementById('map'),
      view: new ol.View({
        center: [7455164.370896921, 5015585.437222827],
        projection: this.projection,
        zoom: 6
      })
    });
  }

  changeLang(language): void {
    this.selected = language;
  }

  getCultureIndex(selected: Culture): number {
    let index = 0;
    Constant.cultures.subscribe(cc => {
      index = cc.indexOf(selected);
    })
    return index;
  }

}
