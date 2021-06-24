package services

import (
	"catalog/core/dto"
	"catalog/core/repositories"
	"context"

	"github.com/brianvoe/gofakeit/v6"
)

type ProductImportService interface {
	ProduceProducts(ctx context.Context) chan dto.ProductDto
	InsertProduct(ctx context.Context, product dto.ProductDto) error
}

type productImportService struct {
	repo repositories.ProductRepository
}

func (service productImportService) ProduceProducts(ctx context.Context) chan dto.ProductDto {
	stream := make(chan dto.ProductDto)

	go func(ctx context.Context, ch chan dto.ProductDto) {
		for i := 0; i < 20; i++ {
			ch <- dto.ProductDto{ID: i, Name: gofakeit.Name()}
		}
	}(ctx, stream)

	return stream
}

func (service productImportService) InsertProduct(ctx context.Context, product dto.ProductDto) error {
	return service.repo.Insert(ctx, dto.NewProduct(product))
}

func NewProductImportService(repo repositories.ProductRepository) ProductImportService {
	return productImportService{repo: repo}
}
