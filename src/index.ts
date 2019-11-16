// Require the framework and instantiate it
import fastify from "fastify"

const server = fastify({logger: true})
server.register(require("./api/endpoints/product.ts"))
// Run the server!
const start = async () => {
    try {
        await server.listen((process.env.PORT || 3000) as number)

        server.log.info(`server listening on ${server.server.address()}`)
    } catch (err) {
        server.log.error(err)
        process.exit(1)
    }
}
start();