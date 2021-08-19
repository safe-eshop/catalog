package cmd

import (
	"catalog/core/services"
	"catalog/core/usecases"
	"catalog/infrastructure/mongodb"
	"catalog/infrastructure/rabbitmq"
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
	f.StringVar(&p.exchange, "exchange", getEnvVarOrDefault("RABBITMQ_EXCHANGE", "catalog"), "rabbitmq exchange")
	f.StringVar(&p.topic, "topic", getEnvVarOrDefault("RABBITMQ_TOPIC", "products"), "rabbitmq import topic")
	f.StringVar(&p.rabbitmqConnection, "rabbitmqConnection", getEnvVarOrDefault("RABBITMQ_CONNECTION", "amqp://guest:guest@127.0.0.1:5672/"), "rabbitmq connection string")
}

func getEnvVarOrDefault(env, def string) string {
	envVar := os.Getenv(env)
	if envVar == "" {
		return def
	}
	return envVar
}

func (p *ImportCatalogProducts) createProductImportUseCase(ctx context.Context, client *rabbitmq.RabbitMqClient) usecases.ProductImportUseCase {
	service := services.NewProductImportService(repositories.NewProductRepository(mongodb.NewClient(p.mongoUrl, ctx)))
	bus := infservices.NewMessageBus(client)
	return usecases.NewProductImportUseCase(service, bus)
}

func (p *ImportCatalogProducts) Execute(ctx context.Context, f *flag.FlagSet, _ ...interface{}) subcommands.ExitStatus {
	rabbitmq, err := rabbitmq.NewRabbitMqClient(p.rabbitmqConnection)
	if err != nil {
		log.WithField("Connection", p.rabbitmqConnection).WithError(err).Fatal("Error when trying connect to rabbitmq broker")
		return subcommands.ExitFailure
	}
	defer rabbitmq.Close()
	usecase := p.createProductImportUseCase(ctx, rabbitmq)
	log.Info("Start import")
	err = usecase.Execute(ctx)
	if err != nil {
		log.WithError(err).Fatalln("Import error")
		return subcommands.ExitFailure
	}
	log.Info("Finish import")
	return subcommands.ExitSuccess
}
