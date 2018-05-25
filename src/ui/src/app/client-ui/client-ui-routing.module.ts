import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ClientUIComponent } from './client-ui.component';
import { MapComponent } from './map/map.component';


const routes: Routes = [

  { path: '', component: ClientUIComponent },
  { path: 'map', component: MapComponent }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class ClientUIRoutingModule { }
