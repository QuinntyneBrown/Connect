import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { baseUrl } from './core/constants';
import { AppRoutingModule } from './app-routing.module';
import { CoreModule } from './core/core.module';

import { AnonymousMasterPageComponent } from './anonymous-master-page.component';
import { MasterPageComponent } from './master-page.component';

import { SharedModule } from './shared/shared.module';
import { LaunchSettings } from './core/launch-settings';
import { map } from 'rxjs/operators';
import { AppStore } from './app-store';
import { LoginModule } from './login/login.module';
import { HomeModule } from './home/home.module';

@NgModule({
  declarations: [AppComponent, AnonymousMasterPageComponent, MasterPageComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    LoginModule,
    HomeModule
  ],
  providers: [
    AppStore,
    { provide: baseUrl, useValue: 'http://localhost:4023/' },
    {
      provide: APP_INITIALIZER,
      useFactory: AppModule.onLaunch,
      multi: true,
      deps: [HttpClient, LaunchSettings]
    }],
  bootstrap: [AppComponent]
})
export class AppModule {
  public static onLaunch(client: HttpClient, launchSettings: LaunchSettings) {
    return function () {
      const _launchSettingsService = launchSettings;      
      client.get("/assets/launchSettings.json")
        .pipe(
          map((result: any) => {            
            launchSettings.logLevel$.next(result.logLevel);
          })
        ).subscribe();
    }
  }
}
