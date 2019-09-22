import { IProductRepository, } from "../../domain/repository/product";
import { Product } from "../../domain/model/product"
export class PostgresProductRepository implements IProductRepository {
    get(id: string): Product {
        throw new Error("Method not implemented.");
    }

}