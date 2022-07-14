import { Component, OnDestroy, OnInit } from '@angular/core';
import { Select, Store } from '@ngxs/store';
import { Observable, Subject, takeUntil } from 'rxjs';
import { FailAction } from 'src/app/store/actions/base.actions';
import { ClearCart } from 'src/app/store/actions/clear-cart.actions';
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
export class CartComponent implements OnInit, OnDestroy {

  @Select(CartState.loading) loadingCart$!: Observable<boolean>;

  loading = false;
  cartProducts: ProductModel[] = [];
  
  private onDestroy$: Subject<void> = new Subject<void>();


  constructor(
    private readonly apiResponse: ApiResponseService,
    private readonly store: Store) { }

  ngOnDestroy(): void {
    this.onDestroy$.next();
    this.onDestroy$.complete();
  }

  ngOnInit(): void {
    this.store.select(CartState.cart).pipe(takeUntil(this.onDestroy$)).subscribe(cart => {
      if(cart == undefined || cart.productIds.length == 0){
        this.cartProducts = [];
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

  clearCart(): void {
    this.store.dispatch(new ClearCart.Start());
  }

}
