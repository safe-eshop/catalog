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

export type ProductsFilter = {}

export interface IProductFilter {
    filter(f: ProductsFilter) : Promise<Option<Product[]>>
}

export class MongoProductFilter implements IProductFilter {

    constructor(private model: Model<MongoProduct>) {
    }
    
    async filter(f: ProductsFilter): Promise<Option<Product[]>> {
        const mongoProducts = await this.model.find().exec();
        
        if (mongoProducts?.length > 0) {
            return some(mongoProducts.map(toDomainProduct))
        }
        
        return none;
    }
    
}