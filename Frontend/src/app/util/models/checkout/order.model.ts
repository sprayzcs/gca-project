export type OrderModel = {
    id: string
    date: Date

    email: string

    cartId: string
    shipmentId?: string

    country: string
    city: string
    zipcode: string
    street: string

    creditCardNumber: string
    creditCardExpiryDate: string
    creditCardVerificationValue: string
}