package services

import (
	"catalog/core/dto"
	"catalog/core/model"
	"context"
)

type ProductCreated struct {
	ID             model.ProductId `json:"id,omitempty"`
	Name           string          `json:"name,omitempty"`
	Brand          string          `json:"brand,omitempty"`
	Description    string          `json:"description,omitempty"`
	Price          float64         `json:"price,omitempty"`
	PromotionPrice *float64        `json:"promotionPrice,omitempty"`
}

func NewProductCreated(dto dto.ProductDto) ProductCreated {
	return ProductCreated{ID: dto.ID, Name: dto.Name, Brand: dto.Brand, Description: dto.Description, Price: dto.Price, PromotionPrice: dto.PromotionPrice}
}

type MessageBus interface {
	Publish(ctx context.Context, p ProductCreated) error
}
