import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CoreModule } from "../core/core.module";
import { AccountRoutingModule } from "./account-routing.module";

import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { FacebookLoginComponent } from './facebook-login/facebook-login.component';

import { AuthService } from "@core/services/account/auth.service";

@NgModule({
  imports: [
    CoreModule,
    FormsModule,
    CommonModule,
    AccountRoutingModule,
  ],

  declarations: [
    RegistrationFormComponent,
    LoginFormComponent,
    FacebookLoginComponent,
  ],

  providers: [ 
    // AuthService 
  ]
})
export class AccountModule { }
