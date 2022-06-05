import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { NbActionsModule,
  NbButtonModule,
  NbCardModule,
  NbIconModule,
  NbInputModule,
  NbLayoutModule,
  NbListModule,
  NbSelectModule,
  NbTagModule,
  NbThemeModule } from '@nebular/theme';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { CartComponent } from './routes/cart/cart.component';
import { HomeComponent } from './routes/home/home.component';
import { ProductCardComponent } from './routes/home/components/product-card/product-card.component';
import { ItemListComponent } from './routes/cart/components/item-list/item-list.component';
import { ContactFormComponent } from './routes/cart/components/contact-form/contact-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ReactiveValidationModule } from 'angular-reactive-validation';

@NgModule({
  declarations: [
    AppComponent,
    CartComponent,
    HomeComponent,
    ProductCardComponent,
    ItemListComponent,
    ContactFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    ReactiveValidationModule,
    NbThemeModule.forRoot({name: 'light'}),
    NbEvaIconsModule,
    NbLayoutModule,
    NbActionsModule,
    NbTagModule,
    NbIconModule,
    NbCardModule,
    NbButtonModule,
    NbListModule,
    NbInputModule,
    NbSelectModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
