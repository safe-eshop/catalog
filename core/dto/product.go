package dto

import "catalog/core/model"

type ProductDto struct {
	ID             model.ProductId `json:"id,omitempty"`
	Name           string          `json:"name,omitempty"`
	Brand          string          `json:"brand,omitempty"`
	Description    string          `json:"description,omitempty"`
	Price          float64         `json:"price,omitempty"`
	PromotionPrice *float64        `json:"promotionPrice,omitempty"`
}

func MapToProductDto(product *model.Product) *ProductDto {
	return &ProductDto{ID: product.ID, Name: product.Name, Brand: product.Brand, Description: product.Description, Price: product.Price, PromotionPrice: product.PromotionPrice}
}

func NewProduct(dto ProductDto) model.Product {
	return model.Product{ID: dto.ID, Name: dto.Name, Brand: dto.Brand, Description: dto.Description, Price: dto.Price, PromotionPrice: dto.PromotionPrice}
}
