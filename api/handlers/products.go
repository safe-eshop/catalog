package handlers

import (
	"catalog/core/services"
	"catalog/infrastructure/repositories"
	"net/http"
	"strconv"

	"github.com/gin-gonic/gin"
)

type catalogHandler struct {
	ProductService services.ProductService
}

func createCatalog() catalogHandler {
	return catalogHandler{ProductService: services.NewProductService(repositories.NewProductRepository())}
}

func StartCatalog(g *gin.Engine) {
	catalog := createCatalog()
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
