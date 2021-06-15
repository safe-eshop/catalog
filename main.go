package main

import (
	"catalog/api/handlers"

	"github.com/gin-gonic/gin"
)

func main() {
	r := gin.Default()
	handlers.StartCatalog(r)
	r.Run() // listen and serve on 0.0.0.0:8080 (for windows "localhost:8080")
}
