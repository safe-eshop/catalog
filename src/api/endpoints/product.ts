import {FastifyInstance} from "fastify"
import { Server, IncomingMessage, ServerResponse } from "http"
import { IProductService } from "../../domain/service/product"
import { ProductService } from "../../infrastructure/service/product"
import { PostgresProductRepository } from "../../infrastructure/repository/product"

export default async function routes(fastify: FastifyInstance<Server, IncomingMessage, ServerResponse>, options: any) {
    fastify.get('/', async (request, reply) => {
        const service: IProductService = new ProductService(new PostgresProductRepository())
        return service.getAll()
      })
}

module.exports = routes