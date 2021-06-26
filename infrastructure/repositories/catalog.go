package repositories

import (
	"catalog/core/model"
	"catalog/core/repositories"
	inframodel "catalog/infrastructure/model"
	"context"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

const ProductsCollectionName = "products"
const CatalogDatabase = "catalog"

type mongoRepo struct {
	client *mongo.Client
	db     *mongo.Database
}

func (repo mongoRepo) GetById(ctx context.Context, id model.ProductId) (*model.Product, error) {
	col := repo.db.Collection(ProductsCollectionName, &options.CollectionOptions{})
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

func (repo mongoRepo) Insert(ctx context.Context, product model.Product) error {
	col := repo.db.Collection(ProductsCollectionName)
	dbProduct := inframodel.NewMongoProduct(product)
	_, err := col.InsertOne(ctx, dbProduct)
	return err
}

func NewProductRepository(client *mongo.Client) repositories.ProductRepository {
	db := client.Database(CatalogDatabase)
	return mongoRepo{client: client, db: db}
}
