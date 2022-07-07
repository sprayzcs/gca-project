import { Component, Input, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { AddToCart } from 'src/app/store/actions/add-to-cart.actions';
import { ProductModel } from 'src/app/util/models/catalog/product.model';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  @Input() product!: ProductModel;

  constructor(private readonly store: Store) { }

  ngOnInit(): void {
  }

  addToCart(productId: string): void {
    this.store.dispatch(new AddToCart.Start(productId));
  }

}
