package main

import "github.com/gin-gonic/gin"
import "strconv"

type Product struct {
	ID          string `json:"id,omitempty"`
	Name        string `json:"name,omitempty"`
	Description string `json:"description,omitempty"`
	Price       float64 `json:"price,omitempty"`
}

type Products struct {
	Items []Product `json:"items,omitempty"`
}

type GetProductsMetadata struct {
	Page int `json:"page,omitempty"`
	PageSize int `json:"pageSize,omitempty"`
	TotalPages int `json:"totalPages,omitempty"`
	TotalProducts int `json:"totalProducts,omitempty"`
}

type GetProductsResult struct {
	Data Products `json:"data,omitempty"`
	Metadata GetProductsMetadata `json:"metadata,omitempty"`
}

func GetAllProducts(page int, pageSize int) GetProductsResult {
	prods := []Product{Product{ID: "Test", Price: 1.2}}
	products := Products{Items: prods}
	meta :=  GetProductsMetadata{ Page: page, PageSize: pageSize, TotalPages: 10, TotalProducts: pageSize * 10}
	return GetProductsResult{ Data: products, Metadata: meta }
}

func main() {
	r := gin.Default()
	r.GET("/api/products", func(c *gin.Context) {
		page := c.DefaultQuery("page", "0")
		pageSize := c.DefaultQuery("pageSize", "12")
		pageI, err := strconv.Atoi(page)
		if err != nil {
			c.Error(err)
		}
		pageSizeI, err := strconv.Atoi(pageSize)
		if err != nil {
			c.Error(err)
		}
		products := GetAllProducts(pageI, pageSizeI)
		c.JSON(200, products)
	})
	r.GET("/ping", func(c *gin.Context) {
		c.JSON(200, gin.H{
			"message": "pong",
		})
	})
	r.Run() // listen and serve on 0.0.0.0:8080
}