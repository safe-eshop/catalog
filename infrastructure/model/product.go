package model

import "catalog/core/model"

type MongoProduct struct {
	ProductID int    `json:"_id,omitempty" bson:"_id,omitempty"`
	Name      string `json:"name,omitempty" bson:"name,omitempty"`
}

func NewMongoProduct(product model.Product) MongoProduct {
	return MongoProduct{ProductID: product.ID, Name: product.Name}
}
func NewMongoProducts(products []model.Product) []MongoProduct {
	result := make([]MongoProduct, len(products))
	for i, product := range products {
		result[i] = NewMongoProduct(product)
	}
	return result
}

func (p *MongoProduct) ToProduct() *model.Product {
	return &model.Product{ID: p.ProductID, Name: p.Name}
}

func ToInterfaceSlice(ss []MongoProduct) []interface{} {
	iface := make([]interface{}, len(ss))
	for i := range ss {
		iface[i] = ss[i]
	}
	return iface
}
