import {Option, map} from "fp-ts/lib/Option";
import {PictureUrl, Product} from "../../domain/model/product";
import {IProductRepository} from "../../domain/repository/product";
import {ProductDto, toProductDto} from "../dto/product";

export default function (repo: IProductRepository) {
    return async (id: string): Promise<Option<ProductDto>> => {
        const result = await repo.get(id);
        return map((product: Product) => toProductDto(product))(result)
    }
}