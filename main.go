package main

import (
	"catalog/core/services"
	"catalog/infrastructure/repositories"
	"net/http"
	"strconv"

	"github.com/gin-gonic/gin"
)

type Catalog struct {
	ProductService services.ProductService
}

func CreateCatalog() Catalog {
	return Catalog{ProductService: services.NewProductService(repositories.NewProductRepository())}
}

func StartCatalog(g *gin.Engine, catalog Catalog) {
	g.GET("/products/:id", func(c *gin.Context) {
		idStr := c.Param("id")
		id, err := strconv.Atoi(idStr)

		if err != nil {
			c.Error(err)
			c.String(http.StatusBadRequest, "bad request")
			return
		}
		result, err := catalog.ProductService.GetById(id)

		if err != nil {
			c.Error(err)
			c.String(http.StatusInternalServerError, "unknown error")
			return
		}
		c.JSON(200, gin.H{
			"id": result.ID,
		})
	})
}

func main() {
	r := gin.Default()
	catalog := CreateCatalog()
	StartCatalog(r, catalog)
	r.Run() // listen and serve on 0.0.0.0:8080 (for windows "localhost:8080")
}
