package mongodb

import (
	"context"

	log "github.com/sirupsen/logrus"

	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

func NewClient(connectionString string, ctx context.Context) *mongo.Client {

	// Set client options
	clientOptions := options.Client().ApplyURI(connectionString)

	// connect to MongoDB
	client, err := mongo.Connect(ctx, clientOptions)

	if err != nil {
		log.WithContext(ctx).WithField("ConnectionString", connectionString).WithError(err).Fatal("Error when trying connect to mongo")
	}

	// Check the connection
	err = client.Ping(ctx, nil)

	if err != nil {
		log.WithContext(ctx).WithField("ConnectionString", connectionString).WithError(err).Fatal("Error when trying ping mongo")
	}

	return client
}
