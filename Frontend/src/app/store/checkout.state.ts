import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { NbGlobalPhysicalPosition, NbToastrService } from "@nebular/theme";
import { Action, Selector, State, StateContext } from "@ngxs/store";
import { BackendService } from "../util/enums/services.enum";
import { CreateOrderModel } from "../util/models/checkout/create-order.model";
import { OrderModel } from "../util/models/checkout/order.model";
import { ApiResponseService } from "../util/services/api-response.service";
import { Checkout } from "./actions/checkout.actions";
import { CheckoutStateModel } from "./models/checkout-state.model";

@Injectable({
    providedIn: 'root'
})
@State<CheckoutStateModel>({
    name: 'CheckoutState',
    defaults: {
        loadingOperations: 0
    }
})
export class CheckoutState {
    
    @Selector()
    static loading(state: CheckoutStateModel): boolean {
        return state.loadingOperations > 0;
    }

    constructor(
        private readonly apiResponseService: ApiResponseService,
        private readonly toastrService: NbToastrService,
        private readonly router: Router
    ) { }

    @Action(Checkout.Start)
    clearCart(context: StateContext<CheckoutStateModel>, action: Checkout.Start): void {
        this.patchLoadingOperations(context, +1);

        this.apiResponseService
            .resolvePost<CreateOrderModel, OrderModel>(
                BackendService.Checkout,
                '',
                action.create,
                () => {console.log('handle')},
                errors => new Checkout.Fail(errors)
            ).subscribe(order => {
                if(order){
                    context.dispatch(new Checkout.Success(order));
                }
            })
    }

    @Action(Checkout.Fail)
    clearCartFail(context: StateContext<CheckoutStateModel>): void {
        this.patchLoadingOperations(context, -1);
    }

    @Action(Checkout.Success)
    clearCartSuccess(context: StateContext<CheckoutStateModel>, action: Checkout.Success): void {
        this.patchLoadingOperations(context, -1);
        this.router.navigate(['confirmation', action.order.id]);
        this.toastrService.success(
            '',
            'Warenkorb wurde bestellt',
            {
                position: NbGlobalPhysicalPosition.BOTTOM_RIGHT,
                duration: 5000,
                hasIcon: true,
                icon: 'shopping-cart-outline'
            });
    }

    private patchLoadingOperations(context: StateContext<CheckoutStateModel>, by: number): void {
        context.patchState({
            loadingOperations: context.getState().loadingOperations + by
        });
    }

}