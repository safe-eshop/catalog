package handlers

import (
	"catalog/core/services"
	"catalog/infrastructure/mongodb"
	"catalog/infrastructure/repositories"
	"context"
	"net/http"
	"strconv"

	"github.com/gin-gonic/gin"
)

type catalogHandler struct {
	ProductService services.ProductService
}

func createCatalog(ctx context.Context) catalogHandler {
	return catalogHandler{ProductService: services.NewProductService(repositories.NewProductRepository(mongodb.NewClient("xD", ctx)))}
}

func StartCatalog(g *gin.Engine) {
	catalog := createCatalog(context.TODO())
	g.GET("/products/:id", func(c *gin.Context) {
		idStr := c.Param("id")
		id, err := strconv.Atoi(idStr)

		if err != nil {
			c.Error(err)
			c.String(http.StatusBadRequest, "bad request")
			return
		}
		result, err := catalog.ProductService.GetById(c.Request.Context(), id)

		if err != nil {
			c.Error(err)
			c.String(http.StatusInternalServerError, "unknown error")
			return
		}

		if result == nil {
			c.Status(404)
			return
		}
		c.JSON(200, gin.H{
			"id": result.ID,
		})
	})
}
