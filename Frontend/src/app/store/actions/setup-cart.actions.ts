import { CartModel } from "src/app/util/models/cart/cart.model";
import { FailAction } from "./base.actions";

export namespace SetupCart {

    export class Start {
        static readonly type = '[Cart] SetupCart.Start';
    }

    export class Fail extends FailAction {
        static readonly type = '[Cart] SetupCart.Fail';
        constructor(public override readonly errorCodes: string[]){
            super(errorCodes);
        }
    }

    export class Success {
        static readonly type = '[Cart] SetupCart.Success';
        constructor(public readonly cart: CartModel) { }
    }

}