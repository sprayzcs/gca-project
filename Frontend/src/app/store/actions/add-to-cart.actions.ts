import { CartModel } from "src/app/util/models/cart/cart.model";
import { FailAction } from "./base.actions"

export namespace AddToCart {

    export class Start {
        static readonly type = '[Cart] AddToCart.Start'
        constructor (public readonly productId: string) {}
    }


    export class Fail extends FailAction {
        static readonly type = '[Cart] AddToCart.Fail'
        constructor (public override readonly errorCodes: string[]) {
            super(errorCodes);
        }
    }


    export class Success {
        static readonly type = '[Cart] AddToCart.Success'
        constructor (public readonly cart: CartModel) { }
    }

}