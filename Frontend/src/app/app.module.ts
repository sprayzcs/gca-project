import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

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
  NbSpinnerModule,
  NbTagModule,
  NbThemeModule, 
  NbToastrModule} from '@nebular/theme';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { CartComponent } from './routes/cart/cart.component';
import { HomeComponent } from './routes/home/home.component';
import { ProductCardComponent } from './routes/home/components/product-card/product-card.component';
import { ItemListComponent } from './shared-components/item-list/item-list.component';
import { ContactFormComponent } from './routes/cart/components/contact-form/contact-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ReactiveValidationModule } from 'angular-reactive-validation';
import { ConfirmationComponent } from './routes/confirmation/confirmation.component';
import { CommonModule } from '@angular/common';
import { PricePipe } from './util/pipes/price.pipe';
import { NgxsModule } from '@ngxs/store';
import { CartState } from './store/cart.state';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    CartComponent,
    HomeComponent,
    ProductCardComponent,
    ItemListComponent,
    ContactFormComponent,
    ConfirmationComponent,
    PricePipe
  ],
  imports: [
    CommonModule, 
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    ReactiveValidationModule,
    HttpClientModule,
    NgxsModule.forRoot([
      CartState
    ]),
    NgxsLoggerPluginModule.forRoot({disabled: environment.production}),
    NgxsReduxDevtoolsPluginModule.forRoot(),
    NbThemeModule.forRoot({name: 'light'}),
    NbToastrModule.forRoot(),
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
    NbSpinnerModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
