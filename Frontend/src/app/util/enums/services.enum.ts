import { environment } from "src/environments/environment";

/// I know this is not an enum, but I use it like one
/// So leave me alone :(
export class BackendService {
    public static Catalog = environment.services.catalog;
    public static Shipping = environment.services.shipping;
    public static Cart = environment.services.cart;
    public static Checkout = environment.services.checkout;
}