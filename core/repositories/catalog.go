package repositories

import (
	"catalog/core/model"
	"context"
)

type ProductRepository interface {
	GetById(id model.ProductId, ctx context.Context) (*model.Product, error)
}
