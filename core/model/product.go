package model

type ProductId = int

type Product struct {
	ID   ProductId
	Name string
}

type Products = []Product
