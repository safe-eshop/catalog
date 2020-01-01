import {Option, map, isSome} from "fp-ts/lib/Option";
import {PictureUrl, Product} from "../../domain/model/product";
import {IProductRepository} from "../../domain/repository/product";
import {ProductDto, toProductDto} from "../dto/product";
import {IProductFilter, ProductsFilter} from "../../infrastructure/repository/product";

export function getProductsUseCase(repo: IProductFilter) {
    return async (filter: ProductsFilter): Promise<Option<ProductDto[]>> => {
        const products = await repo.filter(filter);
        return map<Product[], ProductDto[]>(p => p.map(toProductDto))(products)
    }
}

export function getProductByIdUseCase(repo: IProductRepository) {
    return async (id: string): Promise<Option<ProductDto>> => {
        const result = await repo.get(id);
        return map((product: Product) => toProductDto(product))(result)
    }
}