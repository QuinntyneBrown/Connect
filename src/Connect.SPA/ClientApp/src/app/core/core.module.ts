import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { LocalStorageService } from './local-storage.service';
import { Logger } from './logger.service';
import { HeaderInterceptor } from './headers.interceptor';
import { HubClient } from './hub-client';
import { HubClientGuard } from './hub-client-guard';
import { AuthGuard } from './auth.guard';
import { AuthService } from './auth.service';
import { LoginRedirectService } from './redirect.service';
import { ErrorService } from './error.service';
import { OverlayRefProvider } from './overlay-ref-provider';
import { HttpResponseInterceptor } from './http-response.interceptor';
import { CanDeactivateComponentGuard } from './can-deactivate-component.guard';
import { LaunchSettings } from './launch-settings';

const providers = [
  {
    provide: HTTP_INTERCEPTORS,
    useClass: HeaderInterceptor,
    multi: true
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: HttpResponseInterceptor,
    multi: true
  },

  AuthGuard,
  AuthService,
  CanDeactivateComponentGuard,
  ErrorService,
  HubClient,
  HubClientGuard,
  LaunchSettings,
  LocalStorageService,
  LoginRedirectService,
  Logger,
  OverlayRefProvider
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule
  ],
  providers,
  exports: []
})
export class CoreModule {}
