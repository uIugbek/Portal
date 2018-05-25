import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, XHRBackend } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthService } from "@core/services/account/auth.service";

// import { AuthenticateXHRBackend } from './authenticate-xhr.backend';

import { CoreModule } from './core/core.module';
import { AppRoutingModule } from './app-routing.module';
import { AccountModule } from './account/account.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { ClientUIModule } from './client-ui/client-ui.module';

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,    
  ],

  imports: [
    BrowserModule,
    BrowserAnimationsModule,    
    FormsModule,
    HttpModule,
    CoreModule,
    HttpClientModule,
    AppRoutingModule,
    AccountModule,
    DashboardModule,
    ClientUIModule
  ],

  providers: [
    // { provide: XHRBackend, useClass: AuthenticateXHRBackend },
  ],

  bootstrap: [AppComponent],
  exports: [AppComponent]
})
export class AppModule {}
