import { Product } from "../model/product";

export interface IProductRepository {
    get(id: string): Product | null
}