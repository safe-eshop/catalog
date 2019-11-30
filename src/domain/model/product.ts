export interface ProductDetails {
    readonly weight: number,
    readonly weightUnits: string,
    readonly manufacturer: string,
    readonly color: string
}

export type PictureUrl = string;
export type Price = number;
export type ProductId = string;

export interface ProductInfo {
    readonly name: string,
    readonly slug: string
    readonly brand: string,
    readonly description: string
    readonly picture: PictureUrl
}

export interface Product {
    readonly id: ProductId,
    readonly info: ProductInfo,
    readonly details: ProductDetails,
    readonly price: Price,
}

export function create(id: ProductId, info: ProductInfo, details: ProductDetails, price: Price,) : Product {
    return {id: id, details: details, info: info, price: price} 
}