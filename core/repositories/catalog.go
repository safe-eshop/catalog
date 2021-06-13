package repositories

import "catalog/core/model"

type ProductRepository interface {
	GetById(id model.ProductId) (*model.Product, error)
}
