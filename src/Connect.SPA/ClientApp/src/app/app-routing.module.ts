import {
  Routes,
  RouterModule
} from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MasterPageComponent } from './master-page.component';
import { AnonymousMasterPageComponent } from './anonymous-master-page.component';
import { NgModule } from '@angular/core';
import { HubClientGuard } from './core/hub-client-guard';
import { AuthGuard } from './core/auth.guard';
import { CanDeactivateComponentGuard } from './core/can-deactivate-component.guard';
import { HomePageComponent } from './home/home-page.component';

export const routes: Routes = [
  {
    path: 'login',
    component: AnonymousMasterPageComponent,
    children: [
      {
        path: '',
        component: LoginComponent
      }
    ]
  },
  {
    path: '',
    component: AnonymousMasterPageComponent,
    children: [
      {
        path: '',
        component: HomePageComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
