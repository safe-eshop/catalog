import {Document, Schema, model }  from "mongoose";

export interface ProductDetails extends Document {
    readonly weight: number,
    readonly weightUnits: string,
    readonly manufacturer: string,
    readonly color: string
}

export interface MongoProduct extends Document{
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


export const productModel =  model<MongoProduct>("Product", ProductSchema);