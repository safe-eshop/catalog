import {IProductRepository,} from "../../domain/repository/product";
import {Product} from "../../domain/model/product"
import {Model} from "mongoose";
import {MongoProduct, ProductDetails} from "../model/product"
import {Option, some, none} from 'fp-ts/lib/Option'

export class MongoProductRepository implements IProductRepository {

    constructor(private model: Model<MongoProduct>) {
    }

    async get(id: string): Promise<Option<Product>> {
        const result = await this.model.findOne({_id: id}).exec();
        if (result === null) {
            return none;
        }
        return some({
            id: result._id,
            info: {
                picture: result.picture,
                description: result.description,
                brand: result.brand,
                slug: result.slug,
                name: result.name
            },
            price: result.price,
            details: {
                color: result.details.color,
                manufacturer: result.details.manufacturer,
                weightUnits: result.details.weightUnits,
                weight: result.details.weight
            }
        });
    }

    async insertMany(products: Product[]): Promise<void> {
        const mongoProduct: MongoProduct[] = products.map(p => {
            return {
                _id: p.id,
                name: p.info.name,
                slug: p.info.slug,
                brand: p.info.brand,
                description: p.info.description, details: {
                    weight: p.details.weight,
                    weightUnits: p.details.weightUnits,
                    manufacturer: p.details.manufacturer,
                    color: p.details.color
                },
                price: p.price,
                picture: p.info.picture,
            } as MongoProduct
        });
        await this.model.insertMany(mongoProduct);
    }

    count(): Promise<number> {
        return this.model.count({}).exec();
    }

}