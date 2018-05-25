import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl } from "@angular/forms";
import { State } from '@progress/kendo-data-query';

import * as ol from 'openlayers';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';

import { Constant } from "app/app.constant";

@Component({
  selector: 'client-ui-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
  providers: [  ]
})
export class MapComponent implements OnInit {

  map: ol.Map;
  source = new ol.source.Vector({});
  raster = new ol.layer.Tile({ source: new ol.source.OSM() });
  vector = new ol.layer.Vector({ source: this.source });

  constructor() {
  }

  ngOnInit() {
    this.loadMap();
  }

  loadMap() {
    this.map = new ol.Map({
      layers: [this.raster, this.vector],
      target: 'map',
      view: new ol.View({
        center: [7455164.370896921, 5015585.437222827],
        zoom: 6
      })
    });
  }
}
