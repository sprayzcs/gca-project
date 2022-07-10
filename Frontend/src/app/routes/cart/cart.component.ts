import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Select, Store } from '@ngxs/store';
import { Observable, Subject, takeUntil } from 'rxjs';
import { FailAction } from 'src/app/store/actions/base.actions';
import { Checkout } from 'src/app/store/actions/checkout.actions';
import { ClearCart } from 'src/app/store/actions/clear-cart.actions';
import { CartState } from 'src/app/store/cart.state';
import { CheckoutState } from 'src/app/store/checkout.state';
import { BackendService } from 'src/app/util/enums/services.enum';
import { CartModel } from 'src/app/util/models/cart/cart.model';
import { ProductModel } from 'src/app/util/models/catalog/product.model';
import { CreateOrderModel } from 'src/app/util/models/checkout/create-order.model';
import { ApiResponseService } from 'src/app/util/services/api-response.service';
import { UrlBuilderService } from 'src/app/util/services/url-builder.service';
import { ContactFormComponent } from './components/contact-form/contact-form.component';

@Component({
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit, OnDestroy {

  @Select(CartState.loading) loadingCart$!: Observable<boolean>;
  @Select(CheckoutState.loading) loadingCheckout$!: Observable<boolean>;

  @ViewChild('form') form!: ContactFormComponent;

  loading = false;
  loadingShipping = false;
  cartId: string = '';
  cartProducts: ProductModel[] = [];
  shippingPrice = -1;
  
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
      this.loadShippingPrice(cart);
      this.cartId = cart.id;
    })
  }

  order(): void {
    if(!this.form.isValid){
      return;
    }

    const createOrder: CreateOrderModel = {
      cartId: this.cartId,
      email: this.form.email,
      street: this.form.street,
      zipcode: this.form.zipCode,
      city: this.form.zipCode,
      country: this.form.country,
      creditCardNumber: this.form.creditCardNumber,
      creditCardExpiryDate: this.form.creditCardExpiryDate,
      creditCardVerificationValue: this.form.creditCardCvv
    }

    this.store.dispatch(new Checkout.Start(createOrder));
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

  loadShippingPrice(cart: CartModel){
    this.loadingShipping = true;

    this.apiResponse.resolveGet<number>(
      BackendService.Checkout,
      `shipping/${cart.id}`,
      () => this.loadingShipping = false,
      () => new FailAction([]),
    ).subscribe(shippingPrice => {
      if(shippingPrice){
        this.shippingPrice = shippingPrice;
      }
    })
  }

  isCartEmpty(): boolean {
    return this.cartProducts.length == 0;
  }

  clearCart(): void {
    this.store.dispatch(new ClearCart.Start());
  }

}
