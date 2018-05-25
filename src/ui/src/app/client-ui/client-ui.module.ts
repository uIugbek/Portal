import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';

import { GridModule } from '@progress/kendo-angular-grid';

import { ClientUIRoutingModule } from './client-ui-routing.module';
import { CoreModule } from '../core/core.module';

import { ClientUIComponent } from './client-ui.component';
import { MapComponent } from './map/map.component';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    ClientUIRoutingModule,
    CoreModule,
    GridModule
  ],

  declarations: [
    ClientUIComponent,
    MapComponent,
  ],

  providers: [ 

  ],

  exports: [ 

  ]
})
export class ClientUIModule {
  constructor() {}
}
