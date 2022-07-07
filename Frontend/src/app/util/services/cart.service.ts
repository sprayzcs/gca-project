import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { SetupCart } from "src/app/store/actions/setup-cart.actions";
import { BackendService } from "../enums/services.enum";
import { CartModel } from "../models/cart/cart.model";
import { ApiResponseService } from "./api-response.service";

@Injectable({
    providedIn: 'root'
})
export class CartService {

    constructor(private readonly apiResponseService: ApiResponseService) { }

    public getCartById(cartId: string): Observable<CartModel | undefined> {
        return this.apiResponseService
            .resolveGet<CartModel>(
                BackendService.Cart,
                cartId!, 
                () => {},
                errors => new SetupCart.Fail(errors),
                false);
    }

    public createCart(): Observable<CartModel | undefined> {
        return this.apiResponseService
            .resolvePost<unknown, CartModel>(
                BackendService.Cart,
                '', 
                undefined,
                () => {},
                errors => new SetupCart.Fail(errors),
                false);
    }

}