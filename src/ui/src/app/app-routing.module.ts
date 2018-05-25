import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ClientUIComponent } from './client-ui/client-ui.component';

const appRoutes: Routes = [
  { path: '', component: ClientUIComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
