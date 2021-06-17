package model

import "catalog/core/model"

type MongoProduct struct {
	ProductID int `json:"_id,omitempty" bson:"_id,omitempty"`
}

func (p *MongoProduct) ToProduct() *model.Product {
	return &model.Product{}
}
