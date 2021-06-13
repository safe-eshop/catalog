package services

import (
	"catalog/core/dto"
	"catalog/core/model"
	"catalog/core/repositories"
)

type ProductService interface {
	GetById(id model.ProductId) (*dto.ProductDto, error)
}

type productService struct {
	repo repositories.ProductRepository
}

func (ps productService) GetById(id model.ProductId) (*dto.ProductDto, error) {
	repoRes, err := ps.repo.GetById(id)
	if err != nil {
		return nil, err
	}
	return dto.MapToProductDto(repoRes), nil
}

func NewProductService(repo repositories.ProductRepository) ProductService {
	return productService{repo: repo}
}
