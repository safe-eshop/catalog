package repositories

import "catalog/core/model"

type ProductRepository interface {
	GetProducts() (model.Product, error)
}
