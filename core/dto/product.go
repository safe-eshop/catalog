package dto

import "catalog/core/model"

type ProductDto struct {
	ID model.ProductId
}

func MapToProductDto(product *model.Product) *ProductDto {
	return &ProductDto{}
}
