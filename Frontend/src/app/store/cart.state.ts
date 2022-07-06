import { Injectable } from "@angular/core";
import { Action, Selector, State, StateContext } from "@ngxs/store";
import { CartModel } from "../util/models/cart/cart.model";
import { CartService } from "../util/services/cart.service";
import { SetupCart } from "./actions/setup-cart.actions";
import { CartStateModel } from "./models/cart-state.model";

@Injectable({
    providedIn: 'root'
})
@State<CartStateModel>({
    name: 'CartState',
    defaults: {
        loadingOperations: 0,
        cart: undefined
    }
})
export class CartState {

    @Selector()
    static loading(state: CartStateModel): boolean {
        return state.loadingOperations != 0;
    }

    @Selector()
    static cart(state: CartStateModel): CartModel | undefined {
        return state.cart;
    }

    @Selector()
    static itemAmount(state: CartStateModel): number {
        return state.cart?.productIds.length ?? 0;
    }

    constructor(private readonly cartService: CartService) {}

    @Action(SetupCart.Start)
    setupCart(context: StateContext<CartStateModel>, action: SetupCart.Start): void {
        this.patchLoadingOperations(context, +1);

        const cartId = localStorage.getItem("cartId") as string;

        if(!cartId){
            this.createCart(context);
            return;
        }

        this.cartService.getCartById(cartId).subscribe(cart => {
            if(cart){
                context.dispatch(new SetupCart.Success(cart));
                return;
            }

            this.createCart(context);
        });
    }

    @Action(SetupCart.Fail)
    setupCartFail(context: StateContext<CartStateModel>): void {
        this.patchLoadingOperations(context, -1);
    }

    @Action(SetupCart.Success)
    setupCartSuccess(context: StateContext<CartStateModel>, action: SetupCart.Success): void {
        this.patchLoadingOperations(context, -1);
        context.patchState({ cart: action.cart });
        localStorage.setItem("cartId", action.cart.id);
    }


    private createCart(context: StateContext<CartStateModel>): void {
        this.cartService.createCart().subscribe(cart => {
            if(!cart){
                // TODO: retry? resilience?
                return;
            }

            context.dispatch(new SetupCart.Success(cart));
        });
    }

    private patchLoadingOperations(context: StateContext<CartStateModel>, by: number): void {
        context.patchState({
            loadingOperations: context.getState().loadingOperations + by
        });
    }

}