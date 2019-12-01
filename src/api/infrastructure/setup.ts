import Koa from "koa"
import getProductByIdUseCase from "../../application/usecase/getProductByIdUseCase"
import { MongoProductRepository } from "../../infrastructure/repository/product"
import { productModel } from "../../infrastructure/model/product";
import mongoose from "mongoose";
import { seedDatabaseUseCase } from "../../application/usecase/seed";
import Router from "koa-router";
import { getProductById } from "../endpoints/product";

function getMongoConnectionString() {
    return process.env.MONGO_URL ?? 'mongodb://mongo:27017/catalog?authSource=admin';
}

export function setupMongo() {
    return mongoose.connect(getMongoConnectionString(), { useNewUrlParser: true }).then((_: any) => {
        return seedDatabaseUseCase(new MongoProductRepository(productModel))
    });
}


export default async function setup(app: Koa<Koa.DefaultState, Koa.DefaultContext>): Promise<Router<any, {}>> {
    try {
        const mongo = await setupMongo();
        const router = new Router({ prefix: process.env.PATH_BASE });
        getProductById(getProductByIdUseCase(new MongoProductRepository(productModel)))(router);

        router.get("/", async (ctx, next) => {
            ctx.body = { message: "Test"};
            await next();
        });
        return router;
    }
    catch (rej) {
        console.error(rej);
        process.exit(-1);
    }
}