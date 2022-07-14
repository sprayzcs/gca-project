import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponseService } from 'src/app/util/services/api-response.service';
import { BackendService } from 'src/app/util/enums/services.enum';
import { ProductModel } from 'src/app/util/models/catalog/product.model';
import { FailAction } from 'src/app/store/actions/base.actions';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  loading = false;
  catalog!: Observable<ProductModel[] | undefined>;

  constructor(private readonly apiResponseService: ApiResponseService) { }

  ngOnInit(): void {
    this.loading = true;
    this.catalog = this.apiResponseService.resolveGet<ProductModel[]>(BackendService.Catalog, '', () => {this.loading = false}, () => new FailAction([]));
  }

}
