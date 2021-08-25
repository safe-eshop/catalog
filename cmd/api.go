package cmd

import (
	"catalog/api/handlers"
	"context"
	"flag"
	"os"

	"github.com/gin-gonic/gin"
	"github.com/google/subcommands"
	log "github.com/sirupsen/logrus"
)

type RunCatalogApi struct {
	mongoUrl string
}

func (*RunCatalogApi) Name() string     { return "run-api" }
func (*RunCatalogApi) Synopsis() string { return "run catalog api" }
func (*RunCatalogApi) Usage() string {
	return `go run . run-api`
}

func (p *RunCatalogApi) SetFlags(f *flag.FlagSet) {
	f.StringVar(&p.mongoUrl, "mongoUrl", getEnvVarOrDefault("MONGODB_URL", "mongodb://localhost:27017"), "mongofb connection string")
}

func (p *RunCatalogApi) Execute(ctx context.Context, f *flag.FlagSet, _ ...interface{}) subcommands.ExitStatus {
	log.SetFormatter(&log.JSONFormatter{})
	log.SetOutput(os.Stdout)
	r := gin.Default()
	r.GET("/ping", func(c *gin.Context) {
		c.JSON(200, gin.H{
			"message": "pong",
		})
	})
	cfg := handlers.CatalogStartParameters{MongoDBConnectionString: p.mongoUrl}
	handlers.StartCatalog(ctx, r, cfg)
	err := r.Run() // listen and serve on 0.0.0.0:8080 (for windows "localhost:8080")
	if err != nil {
		return subcommands.ExitFailure
	}
	return subcommands.ExitSuccess
}
