export const environment = {
  production: true,
  services: {
    catalog: window["env"].CATALOG_URL,
    shipping: window["env"].SHIPPING_URL,
    cart: window["env"].CART_URL,
    checkout: window["env"].CHECKOUT_URL,
  }
};
