package services

import (
	"catalog/core/dto"
	"context"
)

type ProductCreated struct {
}

func NewProductCreated(dto dto.ProductDto) ProductCreated {
	return ProductCreated{}
}

type MessageBus interface {
	Publish(ctx context.Context, p ProductCreated) error
}
