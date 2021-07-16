package model

type ProductId = int

type Product struct {
	ID             ProductId
	Name           string
	Brand          string
	Description    string
	Price          float64
	PromotionPrice *float64
}

type Products = []Product
