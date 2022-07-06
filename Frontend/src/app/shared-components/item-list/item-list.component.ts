import { Component, Input, OnInit } from '@angular/core';
import { ProductModel } from 'src/app/util/models/catalog/product.model';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.scss']
})
export class ItemListComponent implements OnInit {

  @Input() products: ProductModel[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
