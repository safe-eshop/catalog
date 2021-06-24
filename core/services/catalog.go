package services

import (
	"catalog/core/dto"
	"catalog/core/model"
	"catalog/core/repositories"
	"context"
)

type ProductService interface {
	GetById(ctx context.Context, id model.ProductId) (*dto.ProductDto, error)
}

type productService struct {
	repo repositories.ProductRepository
}

func (ps productService) GetById(ctx context.Context, id model.ProductId) (*dto.ProductDto, error) {
	repoRes, err := ps.repo.GetById(ctx, id)
	if err != nil {
		return nil, err
	}
	if repoRes == nil {
		return nil, nil
	}
	return dto.MapToProductDto(repoRes), nil
}

func NewProductService(repo repositories.ProductRepository) ProductService {
	return productService{repo: repo}
}
