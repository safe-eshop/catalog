package services

import (
	"catalog/core/services"
	"catalog/infrastructure/rabbitmq"
	"context"
	"encoding/json"

	"github.com/streadway/amqp"
)

type rabbitmqBus struct {
	ExchangeName string
	Topic        string
	Client       *rabbitmq.RabbitMqClient
}

func (b rabbitmqBus) Publish(ctx context.Context, p services.ProductCreated) error {
	err := b.Client.Channel.ExchangeDeclare(
		b.ExchangeName, // name
		"topic",        // type
		true,           // durable
		false,          // auto-deleted
		false,          // internal
		false,          // no-wait
		nil,            // arguments
	)

	if err != nil {
		return err
	}

	body, err := json.Marshal(p)

	if err != nil {
		return err
	}

	b.Client.Channel.Publish(b.ExchangeName, b.Topic, false, false, amqp.Publishing{ContentType: "application/json", Body: body})

	return nil
}

func NewMessageBus(client *rabbitmq.RabbitMqClient, exchange, topic string) rabbitmqBus {
	return rabbitmqBus{Client: client, ExchangeName: exchange, Topic: topic}
}
