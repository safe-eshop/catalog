import {Product} from "../../domain/model/product";

export interface ProductDto {
    readonly id: string,
    readonly name: string,
    readonly slug: string
    readonly brand: string,
    readonly description: string
    readonly picture: string
}

export function toProductDto(product: Product) : ProductDto {
    return {
        id: product.id,
        name: product.info.name,
        brand: product.info.brand,
        description: product.info.description,
        picture: product.info.picture,
        slug: product.info.slug
    }
}