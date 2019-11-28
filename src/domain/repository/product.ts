import { Product } from "../model/product";

export interface IProductRepository {
    get(id: string): Promise<Product | null>
}