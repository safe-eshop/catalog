package repositories

import (
	"catalog/core/model"
	inframodel "catalog/infrastructure/model"
	"context"

	log "github.com/sirupsen/logrus"
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

func (repo mongoRepo) GetByIds(ctx context.Context, ids []model.ProductId) ([]*model.Product, error) {
	col := repo.db.Collection(ProductsCollectionName, &options.CollectionOptions{})
	filter := bson.M{"_id": bson.M{"$in": ids}}
	cur, err := col.Find(ctx, filter)
	if err == mongo.ErrNoDocuments {
		return nil, nil
	} else if err != nil {
		return nil, err
	}

	var results []*model.Product
	for cur.Next(ctx) {
		var result inframodel.MongoProduct
		e := cur.Decode(&result)
		if e != nil {
			log.WithError(e).WithContext(ctx).Errorln("Error when decoding product")
		}
		results = append(results, result.ToProduct())

	}
	return results, nil
}

func (repo mongoRepo) Insert(ctx context.Context, product model.Product) error {
	col := repo.db.Collection(ProductsCollectionName)
	opt := options.Update().SetUpsert(true)
	dbProduct := inframodel.NewMongoProduct(product)
	filter := bson.D{{"_id", dbProduct.ProductID}}
	_, err := col.UpdateOne(ctx, filter, inframodel.ToInsertMongoDocument(dbProduct), opt)
	return err
}

func NewProductRepository(client *mongo.Client) mongoRepo {
	db := client.Database(CatalogDatabase)
	return mongoRepo{client: client, db: db}
}
