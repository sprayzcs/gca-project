import { CartModel } from "src/app/util/models/cart/cart.model";
import { FailAction } from "./base.actions"

export namespace ClearCart {

    export class Start {
        static readonly type = '[Cart] ClearCart.Start'
        constructor () {}
    }


    export class Fail extends FailAction {
        static readonly type = '[Cart] ClearCart.Fail'
        constructor (public override readonly errorCodes: string[]) {
            super(errorCodes);
        }
    }


    export class Success {
        static readonly type = '[Cart] ClearCart.Success'
        constructor (public readonly cart: CartModel) { }
    }

}