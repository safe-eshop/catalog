import {IProductRepository,} from "../../domain/repository/product";
import {Product} from "../../domain/model/product"
import {Model} from "mongoose";
import {MongoProduct} from "../model/product"

export class MongoProductRepository implements IProductRepository {

    constructor(private model: Model<MongoProduct>) {
    }

    async getAll(): Promise<Product[] | null> {
        return [{id: "spierdalaj"}];
    }

    async get(id: string): Promise<Product | null> {
        const result = await this.model.findOne({_id: id}).select({_id: 1, name: 1}).exec();
        if (result === null) {
            return null;
        }
        return {id: result._id}
    }

}