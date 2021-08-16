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
		for i := 1; i < 20; i++ {
			price := gofakeit.Float64Range(10, 64)
			dto := dto.ProductDto{ID: i, Name: gofakeit.Name(), Brand: gofakeit.Car().Brand, Description: gofakeit.BuzzWord(), Price: price}
			if price > 50 {
				promo := price - 10.0
				dto.PromotionPrice = &promo
			}

			ch <- dto

		}
		close(stream)
	}(ctx, stream)

	return stream
}

func (service productImportService) InsertProduct(ctx context.Context, product dto.ProductDto) error {
	return service.repo.Insert(ctx, dto.NewProduct(product))
}

func NewProductImportService(repo repositories.ProductRepository) productImportService {
	return productImportService{repo: repo}
}
