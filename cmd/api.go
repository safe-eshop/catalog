package cmd

import (
	"catalog/api/handlers"
	"context"
	"flag"

	"github.com/gin-gonic/gin"
	"github.com/google/subcommands"
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
	r := gin.Default()
	handlers.StartCatalog(r)
	err := r.Run() // listen and serve on 0.0.0.0:8080 (for windows "localhost:8080")
	if err != nil {
		return subcommands.ExitFailure
	}
	return subcommands.ExitSuccess
}
