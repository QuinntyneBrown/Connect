import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ConversationPageComponent } from './conversation-page.component';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';

const declarations = [
  ConversationPageComponent
];

const providers = [

];

@NgModule({
  declarations: declarations,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

    CoreModule,
    SharedModule
  ],
  providers,
})
export class ConversationsModule { }
