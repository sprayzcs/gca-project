(function (window) {
    window["env"] = window["env"] || {};
    window["env"].CATALOG_URL = '${ENV_CATALOG_URL}'; // not actualized, for local testing
    window["env"].SHIPPING_URL = '${ENV_SHIPPING_URL}'; // not actualized, for local testing
    window["env"].CART_URL = '${ENV_CART_URL}'; // not actualized, for local testing
    window["env"].CHECKOUT_URL = '${ENV_CHECKOUT_URL}'; // not actualized, for local testing
  })(this)