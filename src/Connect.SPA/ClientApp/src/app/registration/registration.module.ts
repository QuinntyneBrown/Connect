import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CreateProfilePageComponent } from './create-profile-page.component';
import { SelectProfileTypePageComponent } from './select-profile-type-page.component';

const declarations = [
  CreateProfilePageComponent,
  SelectProfileTypePageComponent
];

const providers = [

];

@NgModule({
  declarations: declarations,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule	
  ],
  providers,
})
export class RegistrationModule { }
