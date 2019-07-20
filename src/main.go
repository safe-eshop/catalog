package main

import "github.com/gin-gonic/gin"

type Product struct {
	ID          string
	Name        string
	Description string
	Price       float64
}


type Products struct {
	Items []Product
}

func GetAllProducts() Products {
	return Products{Items: []Product{Product{ID: "Test", Price: 1.2}}}
}

func main() {
	r := gin.Default()
	r.GET("/api/products", func(c *gin.Context) {
		products := GetAllProducts()
		c.JSON(200, products)
	})
	r.GET("/ping", func(c *gin.Context) {
		c.JSON(200, gin.H{
			"message": "pong",
		})
	})
	r.Run() // listen and serve on 0.0.0.0:8080
}