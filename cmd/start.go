package main

import (
	"catalog/core/services"
	"catalog/core/usecases"
	"catalog/infrastructure/mongodb"
	"catalog/infrastructure/repositories"
	"context"

	log "github.com/sirupsen/logrus"
)

func createProductImportUseCase(ctx context.Context) usecases.ProductImportUseCase {
	service := services.NewProductImportService(repositories.NewProductRepository(mongodb.NewClient("mongodb://localhost:27017", ctx)))
	return usecases.NewProductImportUseCase(service)
}

func main() {
	ctx := context.TODO()
	usecase := createProductImportUseCase(ctx)
	err := usecase.Execute(ctx)
	if err != nil {
		log.WithError(err).Fatalln("Import error")
	}
}
