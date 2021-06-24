package dto

import "catalog/core/model"

type ProductDto struct {
	ID   model.ProductId
	Name string
}

func MapToProductDto(product *model.Product) *ProductDto {
	return &ProductDto{}
}

func NewProduct(dto ProductDto) model.Product {
	return model.Product{}
}
