import { CartModel } from "src/app/util/models/cart/cart.model";

export type CartStateModel = {
    loadingOperations: number;
    cart?: CartModel;
}