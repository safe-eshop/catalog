import { IProductRepository } from "../../domain/repository/product";

export class PostgresProductRepository implements IProductRepository {
    get(id: string): import("../../domain/model/product").Product {
        throw new Error("Method not implemented.");
    }

}