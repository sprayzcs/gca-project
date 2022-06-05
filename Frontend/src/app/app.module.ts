import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { NbActionsModule, NbButtonModule, NbCardModule, NbIconModule, NbLayoutModule, NbListModule, NbTagModule, NbThemeModule } from '@nebular/theme';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { CartComponent } from './routes/cart/cart.component';
import { HomeComponent } from './routes/home/home.component';
import { ProductCardComponent } from './routes/home/components/product-card/product-card.component';
import { ItemListComponent } from './routes/cart/components/item-list/item-list.component';

@NgModule({
  declarations: [
    AppComponent,
    CartComponent,
    HomeComponent,
    ProductCardComponent,
    ItemListComponent
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
    NbButtonModule,
    NbListModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
