package repositories

import (
	"catalog/core/model"
	"catalog/core/repositories"
	"errors"
)

type fakeRepo struct {
}

func (repo fakeRepo) GetById(id model.ProductId) (*model.Product, error) {
	return nil, errors.New("xD")
}

func NewProductRepository() repositories.ProductRepository {
	return fakeRepo{}
}
