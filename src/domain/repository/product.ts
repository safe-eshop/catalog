import {Product, ProductId} from "../model/product";
import {Option} from "fp-ts/lib/Option";
import {eitherT} from "fp-ts/lib";
export interface IProductRepository {
    get(id: ProductId): Promise<Option<Product>>
    count() : Promise<number>
    insertMany(products: Product[]): Promise<void>;
}