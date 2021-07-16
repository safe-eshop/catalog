package services

import (
	"catalog/core/services"
	"context"
)

type fakeBus struct {
}

func (b fakeBus) Publish(ctx context.Context, p services.ProductCreated) error {
	return nil
}

func NewMessageBus() services.MessageBus {
	return fakeBus{}
}
