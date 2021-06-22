package repositories

import (
	"catalog/core/model"
	"catalog/core/repositories"
	inframodel "catalog/infrastructure/model"
	"context"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
)

const ProductsCollectionName = "products"

type mongoRepo struct {
	client *mongo.Client
	db     *mongo.Database
}

func (repo mongoRepo) GetById(ctx context.Context, id model.ProductId) (*model.Product, error) {
	col := repo.db.Collection(ProductsCollectionName)
	filter := bson.D{{"_id", id}}
	var result inframodel.MongoProduct
	err := col.FindOne(ctx, filter).Decode(&result)
	if err == mongo.ErrNoDocuments {
		return nil, nil
	} else if err != nil {
		return nil, err
	}

	return result.ToProduct(), nil
}

func (repo mongoRepo) Insert(ctx context.Context, products model.Products) error {
	col := repo.db.Collection(ProductsCollectionName)
	dbProducts := inframodel.NewMongoProducts(products)
	_, err := col.InsertMany(ctx, inframodel.ToInterfaceSlice(dbProducts))
	return err
}

func NewProductRepository(client *mongo.Client) repositories.ProductRepository {
	return mongoRepo{client: client}
}
