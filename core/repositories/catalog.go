package repositories

import (
	"catalog/core/model"
	"context"
)

type ProductRepository interface {
	GetById(ctx context.Context, id model.ProductId) (*model.Product, error)
	Insert(ctx context.Context, products model.Product) error
}
