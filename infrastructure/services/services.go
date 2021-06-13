package services

import (
	"catalog/core/dto"
	"catalog/core/model"
	"catalog/core/services"
	"errors"
)

type fakeService struct {
}

func (fs fakeService) GetById(id model.ProductId) (*dto.ProductDto, error) {
	return nil, errors.New("xD")
}

func NewProductService() services.ProductService {
	return fakeService{}
}
