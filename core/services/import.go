package services

import (
	"catalog/core/dto"
	"catalog/core/repositories"
	"context"
)

type ProductImportService interface {
	ProduceProducts(ctx context.Context) chan dto.ProductDto
	InsertProduct(product dto.ProductDto) error
}

type productImportService struct {
	repo repositories.ProductRepository
}

func (service productImportService) ProduceProducts(ctx context.Context) chan dto.ProductDto {
	return make(chan dto.ProductDto)
}

func (service productImportService) InsertProduct(product dto.ProductDto) error {
	return nil
}

func NewProductImportService(repo repositories.ProductRepository) ProductImportService {
	return productImportService{repo: repo}
}
