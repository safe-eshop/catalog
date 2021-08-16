package services

import (
	"catalog/core/dto"
	"catalog/core/model"
	"catalog/core/repositories"
	"context"
)

type ProductService interface {
	GetById(ctx context.Context, id model.ProductId) (*dto.ProductDto, error)
	GetByIds(ctx context.Context, ids []model.ProductId) ([]*dto.ProductDto, error)
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

func (ps productService) GetByIds(ctx context.Context, ids []model.ProductId) ([]*dto.ProductDto, error) {
	repoRes, err := ps.repo.GetByIds(ctx, ids)
	if err != nil {
		return nil, err
	}
	if repoRes == nil {
		return nil, nil
	}
	res := make([]*dto.ProductDto, len(repoRes))
	for i, prod := range repoRes {
		res[i] = dto.MapToProductDto(prod)
	}
	return res, nil
}

func NewProductService(repo repositories.ProductRepository) productService {
	return productService{repo: repo}
}
