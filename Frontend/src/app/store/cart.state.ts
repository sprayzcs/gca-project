import { Injectable } from "@angular/core";
import { NbGlobalPhysicalPosition, NbToastrService } from "@nebular/theme";
import { Action, Selector, State, StateContext } from "@ngxs/store";
import { BackendService } from "../util/enums/services.enum";
import { CartModel } from "../util/models/cart/cart.model";
import { ApiResponseService } from "../util/services/api-response.service";
import { CartService } from "../util/services/cart.service";
import { AddToCart } from "./actions/add-to-cart.actions";
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

    constructor(
        private readonly cartService: CartService,
        private readonly apiResponseService: ApiResponseService,
        private readonly toastrService: NbToastrService
    ) {}

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


    @Action(AddToCart.Start)
    addItemToCart(context: StateContext<CartStateModel>, action: AddToCart.Start): void {
        this.patchLoadingOperations(context, +1);

        const state = context.getState();
        this.apiResponseService
            .resolvePatch<unknown, CartModel>(
                BackendService.Cart,
                `${state.cart!.id}/${action.productId}`,
                undefined,
                () => {console.log('handle')},
                errors => new AddToCart.Fail(errors)
            ).subscribe(cart => {
                if(cart){
                    context.dispatch(new AddToCart.Success(cart));
                }
            })
    }

    @Action(AddToCart.Fail)
    addItemToCartFail(context: StateContext<CartStateModel>): void {
        this.patchLoadingOperations(context, -1);
    }

    @Action(AddToCart.Success)
    addItemToCartSuccess(context: StateContext<CartStateModel>, action: AddToCart.Success): void {
        this.patchLoadingOperations(context, -1);
        context.patchState({ cart: action.cart });
        this.toastrService.success(
            '',
            'In den Warenkorb gelegt',
            {
                position: NbGlobalPhysicalPosition.BOTTOM_RIGHT,
                duration: 5000,
                hasIcon: true,
                icon: 'shopping-cart-outline'
            });
    }



    private patchLoadingOperations(context: StateContext<CartStateModel>, by: number): void {
        context.patchState({
            loadingOperations: context.getState().loadingOperations + by
        });
    }

}