import {Product, ProductId} from "../model/product";
import {Option} from "fp-ts/lib/Option";

export interface IProductRepository {
    get(id: ProductId): Promise<Option<Product>>
    count() : Promise<number>
    insertMany(products: Product[]): Promise<void>;
}