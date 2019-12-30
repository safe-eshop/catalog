import { Document, Schema, model } from "mongoose";
import { Product } from "../../domain/model/product";

export interface ProductDetails {
    readonly weight: number,
    readonly weightUnits: string,
    readonly manufacturer: string,
    readonly color: string
}

export interface MongoProduct extends Document {
    readonly _id: string,
    readonly name: string,
    readonly slug: string
    readonly brand: string,
    readonly description: string
    readonly details: ProductDetails,
    readonly price: number,
    readonly picture: string
}

export const ProductDetailsSchema = new Schema<ProductDetails>({
    weight: { required: true, type: Number },
    weightUnits: { required: true, type: String },
    manufacturer: { required: true, type: String },
    color: { required: true, type: String }
});

export const ProductSchema = new Schema<MongoProduct>({
    _id: { type: String },
    name: { required: true, type: String },
    slug: { required: true, type: String },
    brand: { required: true, type: String },
    description: { required: true, type: String },
    details: ProductDetailsSchema,
    price: { required: true, type: Number },
    picture: { required: true, type: String }
});


export function toDomainProduct(mongoProduct: MongoProduct): Product {
    return {
        id: mongoProduct._id,
        info: {
            picture: mongoProduct.picture,
            description: mongoProduct.description,
            brand: mongoProduct.brand,
            slug: mongoProduct.slug,
            name: mongoProduct.name
        },
        price: mongoProduct.price,
        details: {
            color: mongoProduct.details.color,
            manufacturer: mongoProduct.details.manufacturer,
            weightUnits: mongoProduct.details.weightUnits,
            weight: mongoProduct.details.weight
        }
    }
}

export function toMongoProduct(p: Product): MongoProduct {
    return {
        _id: p.id,
        name: p.info.name,
        slug: p.info.slug,
        brand: p.info.brand,
        description: p.info.description, 
        details: {
            weight: p.details.weight,
            weightUnits: p.details.weightUnits,
            manufacturer: p.details.manufacturer,
            color: p.details.color
        },
        price: p.price,
        picture: p.info.picture,
    } as MongoProduct;
}

export const productModel = model<MongoProduct & Document>("Product", ProductSchema);