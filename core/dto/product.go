package dto

import "catalog/core/model"

type ProductDto struct {
	ID   model.ProductId `json:"id,omitempty"`
	Name string          `json:"name,omitempty"`
}

func MapToProductDto(product *model.Product) *ProductDto {
	return &ProductDto{ID: product.ID, Name: product.Name}
}

func NewProduct(dto ProductDto) model.Product {
	return model.Product{ID: dto.ID, Name: dto.Name}
}
