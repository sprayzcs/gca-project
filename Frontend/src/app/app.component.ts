import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { SetupCart } from './store/actions/setup-cart.actions';
import { CartState } from './store/cart.state';
import { CartModel } from './util/models/cart/cart.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  
  @Select(CartState.loading) loading$!: Observable<boolean>;
  @Select(CartState.itemAmount) itemAmount$!: Observable<number>;

  constructor(
    private readonly router: Router,
    private readonly store: Store
  ) {}

  ngOnInit(): void {
    if(!this.store.selectSnapshot(CartState.cart)){
      this.store.dispatch(new SetupCart.Start());
    }
  }

  gotoCart(): void {
    this.router.navigate(['/cart']);
  }

}
