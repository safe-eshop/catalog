import {IProductService} from "../../domain/service/product";
import {Option, isSome, map} from "fp-ts/lib/Option";
import {Product} from "../../domain/model/product";


export interface ProductDto {
    readonly id: string
}

export default function (service: IProductService) {
    return async (id: string) : Promise<Option<ProductDto>> => {
        const result = await service.getById(id);
        return map((product: Product) => { return { id: product.id } as ProductDto})(result)
    }
}