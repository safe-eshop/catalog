package repositories

import (
	"catalog/core/model"
	"catalog/core/repositories"
	"context"

	"go.mongodb.org/mongo-driver/mongo"
)

const ProductsCollectionName = "products"

type mongoRepo struct {
	client *mongo.Client
	db     *mongo.Database
}

func (repo mongoRepo) GetById(id model.ProductId, ctx context.Context) (*model.Product, error) {
	col := repo.db.Collection(ProductsCollectionName)
	col.FindOne(id)
}

func NewProductRepository(client *mongo.Client) repositories.ProductRepository {
	return mongoRepo{client: client}
}
