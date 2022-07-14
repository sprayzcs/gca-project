import { Component, OnDestroy, OnInit } from '@angular/core';
import { delayWhen, skipUntil, Subject, takeUntil } from 'rxjs';
import { ApiResponseService } from 'src/app/util/services/api-response.service';
import { BackendService } from 'src/app/util/enums/services.enum';
import { ProductModel } from 'src/app/util/models/catalog/product.model';
import { FailAction } from 'src/app/store/actions/base.actions';
import { Store } from '@ngxs/store';
import { CartState } from 'src/app/store/cart.state';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {

  loading = false;
  initialized = false;
  showProducts: ProductModel[] = [];
  
  private catalog: ProductModel[] = [];
  private onDestroy$ = new Subject<void>();

  constructor(
    private readonly apiResponseService: ApiResponseService,
    private readonly store: Store
  ) { }

  ngOnDestroy(): void {
    this.onDestroy$.next();
    this.onDestroy$.complete();
  }

  ngOnInit(): void {
    this.loading = true;
    this.apiResponseService.resolveGet<ProductModel[]>(
      BackendService.Catalog,
      '',
      () => {this.loading = false}, () => new FailAction([]),
    ).subscribe(catalog => {
      if(catalog){
        this.catalog = catalog;
      }

      this.startListenOnCart();
    });

  }
  
  startListenOnCart(): void {
    this.store
      .select(CartState.cart)
      .pipe(
        takeUntil(this.onDestroy$),
      )
      .subscribe(cart => {
        this.initialized = true;
        this.showProducts = this.catalog.filter(p => !cart?.productIds.includes(p.id));
      });
  }

  displayProducts(): boolean {
    return this.initialized && !this.loading && this.showProducts.length != 0;
  }

  get allProductsInCar(): boolean {
    return this.initialized && this.showProducts.length == 0;
  }

  get noProducts(): boolean {
    return this.initialized && this.catalog.length == 0;
  }

}
