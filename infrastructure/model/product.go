package model

import (
	"catalog/core/model"

	"go.mongodb.org/mongo-driver/bson"
)

type MongoProduct struct {
	ProductID      int      `json:"_id,omitempty" bson:"_id,omitempty"`
	Name           string   `json:"name,omitempty" bson:"name,omitempty"`
	Brand          string   `json:"brand,omitempty" bson:"brand,omitempty"`
	Description    string   `json:"description,omitempty" bson:"description,omitempty"`
	Price          float64  `json:"price,omitempty" bson:"price,omitempty"`
	PromotionPrice *float64 `json:"promotionPrice,omitempty" bson:"promotionPrice,omitempty"`
}

func NewMongoProduct(product model.Product) MongoProduct {
	return MongoProduct{ProductID: product.ID, Name: product.Name, Brand: product.Brand, Description: product.Description, Price: product.Price, PromotionPrice: product.PromotionPrice}
}

func ToInsertMongoDocument(product MongoProduct) bson.M {
	p := bson.M{
		"_id":            product.ProductID,
		"name":           product.Name,
		"brand":          product.Brand,
		"description":    product.Description,
		"price":          product.Price,
		"promotionPrice": product.PromotionPrice,
	}
	return bson.M{"$set": p}
}
func NewMongoProducts(products []model.Product) []MongoProduct {
	result := make([]MongoProduct, len(products))
	for i, product := range products {
		result[i] = NewMongoProduct(product)
	}
	return result
}

func (p *MongoProduct) ToProduct() *model.Product {
	return &model.Product{ID: p.ProductID, Name: p.Name, Brand: p.Brand, Description: p.Description, Price: p.Price, PromotionPrice: p.PromotionPrice}
}

func ToInterfaceSlice(ss []MongoProduct) []interface{} {
	iface := make([]interface{}, len(ss))
	for i := range ss {
		iface[i] = ss[i]
	}
	return iface
}
