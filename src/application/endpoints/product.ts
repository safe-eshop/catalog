import {FastifyInstance} from "fastify"
import { Server, IncomingMessage, ServerResponse } from "http"

export default async function routes(fastify: FastifyInstance<Server, IncomingMessage, ServerResponse>, options: any) {
    fastify.get('/', async (request, reply) => {
        return { hello: 'world' }
      })
}

module.exports = routes