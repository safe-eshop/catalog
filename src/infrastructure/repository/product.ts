import { IProductRepository, } from "../../domain/repository/product";
import { Product } from "../../domain/model/product"
export class PostgresProductRepository implements IProductRepository {
    async getAll(): Promise<Product[] | null> {
        return [{ id: "spierdalaj" }];
    }
    async get(id: string): Promise<Product | null> {
        return { id: "spierdalaj" }
    }

}