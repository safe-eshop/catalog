import { IProductRepository, } from "../../domain/repository/product";
import { Product } from "../../domain/model/product"
export class PostgresProductRepository implements IProductRepository {
    getAll(): Promise<Product[] | null> {
        throw new Error("Method not implemented.");
    }
    get(id: string): Promise<Product | null> {
        throw new Error("Method not implemented.");
    }

}