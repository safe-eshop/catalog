package cmd

import (
	"catalog/core/services"
	"catalog/core/usecases"
	"catalog/infrastructure/mongodb"
	"catalog/infrastructure/repositories"
	infservices "catalog/infrastructure/services"
	"context"
	"flag"
	"os"

	"github.com/google/subcommands"
	log "github.com/sirupsen/logrus"
)

type ImportCatalogProducts struct {
	mongoUrl           string
	rabbitmqConnection string
	exchange           string
	topic              string
}

func (*ImportCatalogProducts) Name() string     { return "import-products" }
func (*ImportCatalogProducts) Synopsis() string { return "import catalog products" }
func (*ImportCatalogProducts) Usage() string {
	return `go run . import-products`
}

func (p *ImportCatalogProducts) SetFlags(f *flag.FlagSet) {
	f.StringVar(&p.mongoUrl, "mongoUrl", getEnvVarOrDefault("MONGODB_URL", "mongodb://localhost:27017"), "mongofb connection string")
	f.StringVar(&p.exchange, "exchange", getEnvVarOrDefault("RABBIT_EXCHANGE", "mongodb://localhost:27017"), "mongofb connection string")
	f.StringVar(&p.topic, "topic", getEnvVarOrDefault("MONGODB_URL", "mongodb://localhost:27017"), "mongofb connection string")
	f.StringVar(&p.rabbitmqConnection, "rabbitmqConnection", getEnvVarOrDefault("MONGODB_URL", "mongodb://localhost:27017"), "mongofb connection string")
}

func getEnvVarOrDefault(env, def string) string {
	envVar := os.Getenv(env)
	if envVar == "" {
		return def
	}
	return envVar
}

func (p *ImportCatalogProducts) createProductImportUseCase(ctx context.Context) usecases.ProductImportUseCase {
	service := services.NewProductImportService(repositories.NewProductRepository(mongodb.NewClient(p.mongoUrl, ctx)))
	bus := infservices.NewMessageBus()
	return usecases.NewProductImportUseCase(service, bus)
}

func (p *ImportCatalogProducts) Execute(ctx context.Context, f *flag.FlagSet, _ ...interface{}) subcommands.ExitStatus {
	usecase := p.createProductImportUseCase(ctx)
	log.Info("Start import")
	err := usecase.Execute(ctx)
	if err != nil {
		log.WithError(err).Fatalln("Import error")
		return subcommands.ExitFailure
	}
	log.Info("Finish import")
	return subcommands.ExitSuccess
}
