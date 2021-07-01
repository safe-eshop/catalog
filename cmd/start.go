package main

import (
	"catalog/core/services"
	"catalog/core/usecases"
	"catalog/infrastructure/mongodb"
	"catalog/infrastructure/repositories"
	infservices "catalog/infrastructure/services"
	"context"

	log "github.com/sirupsen/logrus"
)

func createProductImportUseCase(ctx context.Context) usecases.ProductImportUseCase {
	service := services.NewProductImportService(repositories.NewProductRepository(mongodb.NewClient("mongodb://localhost:27017", ctx)))
	bus := infservices.NewMessageBus()
	return usecases.NewProductImportUseCase(service, bus)
}

func main() {
	ctx := context.TODO()
	usecase := createProductImportUseCase(ctx)
	log.Info("Start import")
	err := usecase.Execute(ctx)
	if err != nil {
		log.WithError(err).Fatalln("Import error")
		return
	}
	log.Info("Finish import")
}
