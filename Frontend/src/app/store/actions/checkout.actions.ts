import { CartModel } from "src/app/util/models/cart/cart.model";
import { CreateOrderModel } from "src/app/util/models/checkout/create-order.model";
import { OrderModel } from "src/app/util/models/checkout/order.model";
import { FailAction } from "./base.actions"

export namespace Checkout {

    export class Start {
        static readonly type = '[Cart] Checkout.Start'
        constructor (public readonly create: CreateOrderModel) {}
    }


    export class Fail extends FailAction {
        static readonly type = '[Cart] Checkout.Fail'
        constructor (public override readonly errorCodes: string[]) {
            super(errorCodes);
        }
    }


    export class Success {
        static readonly type = '[Cart] Checkout.Success'
        constructor (public readonly order: OrderModel) { }
    }

}