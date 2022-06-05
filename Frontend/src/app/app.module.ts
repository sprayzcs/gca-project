import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { NbActionsModule, NbButtonModule, NbCardModule, NbIconModule, NbLayoutModule, NbTagModule, NbThemeModule } from '@nebular/theme';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { CartComponent } from './routes/cart/cart.component';
import { HomeComponent } from './routes/home/home.component';
import { ProductCardComponent } from './routes/home/components/product-card/product-card.component';

@NgModule({
  declarations: [
    AppComponent,
    CartComponent,
    HomeComponent,
    ProductCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NbThemeModule.forRoot({name: 'light'}),
    NbEvaIconsModule,
    NbLayoutModule,
    NbActionsModule,
    NbTagModule,
    NbIconModule,
    NbCardModule,
    NbButtonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
