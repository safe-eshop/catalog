package services

import (
	"catalog/core/dto"
	"catalog/core/model"
)

type ProductService interface {
	GetById(id model.ProductId) (*dto.ProductDto, error)
}
