import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { raceWith } from 'rxjs';
import { FailAction } from 'src/app/store/actions/base.actions';
import { BackendService } from 'src/app/util/enums/services.enum';
import { CartModel } from 'src/app/util/models/cart/cart.model';
import { ProductModel } from 'src/app/util/models/catalog/product.model';
import { OrderModel } from 'src/app/util/models/checkout/order.model';
import { ShipmentModel } from 'src/app/util/models/shipment/shipment.model';
import { ApiResponseService } from 'src/app/util/services/api-response.service';
import { CartService } from 'src/app/util/services/cart.service';
import { UrlBuilderService } from 'src/app/util/services/url-builder.service';

@Component({
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.scss']
})
export class ConfirmationComponent implements OnInit {

  loading: boolean = false;
  loadingCart: boolean = false;
  loadingShipping: boolean = false;
  orderId: string = '';
  order?: OrderModel;
  shipment?: ShipmentModel;
  products: ProductModel[] = [];

  constructor(
    private readonly apiResponseService: ApiResponseService,
    private readonly route: ActivatedRoute,
    private readonly cartService: CartService
  ) { }

  ngOnInit(): void {
    this.orderId = this.route.snapshot.paramMap.get('orderId')!;

    this.loadOrder();
  }

  loadOrder(): void {
    this.loading = true;
    this.apiResponseService.resolveGet<OrderModel>(
        BackendService.Checkout,
        `${this.orderId}`,
        () => this.loading = true,
        errors => new FailAction(errors)
      ).subscribe(order => {
        if(!order){
          this.loading = false;
          return;
        }

        this.order = order;
        this.loading = false;
        this.loadCart(order.cartId);
        if(order.shipmentId){
          this.loadShipping(order.shipmentId);
        }
      });
  }

  loadCart(cartId: string): void {
    this.loadingCart = true;
    this.cartService.getCartById(cartId).subscribe(cart => {
      if(!cart){
        this.loadingCart = false;
        return;
      }

      this.loadCartItems(cart);
    });
  }

  loadCartItems(cart: CartModel): void {    
    this.apiResponseService.resolveGet<ProductModel[]>(
      BackendService.Catalog,
      'list' + UrlBuilderService.buildQueryStringWithArray('productIds', cart.productIds),
      () => this.loading = false,
      () => new FailAction([]))
      .subscribe(products => {
        if(products){
          this.products = products;
        }
        this.loadingCart = false;
      });
  }

  loadShipping(shippingId: string): void {
    this.apiResponseService.resolveGet<ShipmentModel>(
      BackendService.Shipping,
      `${shippingId}`,
      () => this.loadingShipping = true,
      errors => new FailAction(errors),
      false
    ).subscribe(shipment => {
      if(!shipment){
        this.loading = false;
        return;
      }

      this.shipment = shipment;
      this.loadingShipping = false;
    });
  }

}
