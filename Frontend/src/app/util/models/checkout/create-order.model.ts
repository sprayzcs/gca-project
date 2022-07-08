export type CreateOrderModel = {
    cartId: string

    email: string

    country: string
    city: string
    zipcode: string
    street: string

    creditCardNumber: string
    creditCardExpiryDate: Date
    creditCardVerificationValue: string
}