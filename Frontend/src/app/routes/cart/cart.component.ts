import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { FailAction } from 'src/app/store/actions/base.actions';
import { CartState } from 'src/app/store/cart.state';
import { BackendService } from 'src/app/util/enums/services.enum';
import { CartModel } from 'src/app/util/models/cart/cart.model';
import { ProductModel } from 'src/app/util/models/catalog/product.model';
import { ApiResponseService } from 'src/app/util/services/api-response.service';
import { UrlBuilderService } from 'src/app/util/services/url-builder.service';

@Component({
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  loading = false;
  cartProducts: ProductModel[] = [];

  constructor(
    private readonly apiResponse: ApiResponseService,
    private readonly store: Store) { }

  ngOnInit(): void {
    this.store.select(CartState.cart).subscribe(cart => {
      if(!cart){
        return;
      }
    
      this.loadCartItems(cart);
    })
  }

  loadCartItems(cart: CartModel): void {
    this.loading = true;
    this.apiResponse.resolveGet<ProductModel[]>(
      BackendService.Catalog,
      'list' + UrlBuilderService.buildQueryStringWithArray('productIds', cart.productIds),
      () => this.loading = false,
      () => new FailAction([]))
      .subscribe(products => {
        if(products){
          this.cartProducts = products;
        }
      });
  }

  isCartEmpty(): boolean {
    return this.cartProducts.length == 0;
  }

}
