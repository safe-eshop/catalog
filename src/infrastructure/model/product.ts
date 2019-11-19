import {Document, Schema, model }  from "mongoose";

export interface MongoProduct extends Document{
    readonly _id: string,
    readonly name: string
}

export const ProductSchema = new Schema<MongoProduct>({
    name: { required: true, type: String  }
});

export const productModel =  model<MongoProduct>("Product", ProductSchema);