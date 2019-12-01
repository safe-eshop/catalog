import {Option, isSome, map} from "fp-ts/lib/Option";
import {Product} from "../../domain/model/product";
import { IProductRepository } from "../../domain/repository/product";


export interface ProductDto {
    readonly id: string,
    readonly name: string,
}

export default function (repo: IProductRepository) {
    return async (id: string) : Promise<Option<ProductDto>> => {
        const result = await repo.get(id);
        return map((product: Product) => { return { id: product.id, name: product.info.name } as ProductDto})(result)
    }
}