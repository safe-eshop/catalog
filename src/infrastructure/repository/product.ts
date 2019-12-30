import {IProductRepository,} from "../../domain/repository/product";
import {Product} from "../../domain/model/product"
import {Model} from "mongoose";
import {MongoProduct, ProductDetails, toDomainProduct, toMongoProduct} from "../model/product"
import {Option, some, none} from 'fp-ts/lib/Option'

export class MongoProductRepository implements IProductRepository {

    constructor(private model: Model<MongoProduct>) {
    }

    async get(id: string): Promise<Option<Product>> {
        const result = await this.model.findOne({_id: id}).exec();
        if (result === null) {
            return none;
        }
        return some(toDomainProduct(result));
    }

    async insertMany(products: Product[]): Promise<void> {
        const mongoProduct: MongoProduct[] = products.map(toMongoProduct);
        await this.model.insertMany(mongoProduct);
    }

    count(): Promise<number> {
        return this.model.count({}).exec();
    }

}