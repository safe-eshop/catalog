import Koa from "koa"
import {MongoProductFilter, MongoProductRepository} from "../../infrastructure/repository/product"
import { productModel } from "../../infrastructure/model/product";
import mongoose from "mongoose";
import { seedDatabaseUseCase } from "../../application/usecase/seed";
import Router from "koa-router";
import {getProductByIdEndpoint, getProductsEndpoint} from "../endpoints/product";
import {getProductByIdUseCase, getProductsUseCase} from "../../application/usecase/product";

function getMongoConnectionString() {
    return process.env.MONGOsURL ?? 'mongodb://root:test@localhost:27017/catalog?authSource=admin';
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
        getProductByIdEndpoint(getProductByIdUseCase(new MongoProductRepository(productModel)))(router);
        getProductsEndpoint(getProductsUseCase(new MongoProductFilter(productModel)))(router);
        
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