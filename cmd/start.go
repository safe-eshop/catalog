package main

import (
	"catalog/core/services"
	"catalog/core/usecases"
	"catalog/infrastructure/mongodb"
	"catalog/infrastructure/repositories"
	infservices "catalog/infrastructure/services"
	"context"
	"os"

	log "github.com/sirupsen/logrus"
)

func getEnvVarOrDefault(env, def string) string {
	envVar := os.Getenv(env)
	if envVar == "" {
		return def
	}
	return envVar
}

func createProductImportUseCase(ctx context.Context) usecases.ProductImportUseCase {
	mongoUrl := getEnvVarOrDefault("MONGODB_URL", "mongodb://localhost:27017")
	service := services.NewProductImportService(repositories.NewProductRepository(mongodb.NewClient(mongoUrl, ctx)))
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
