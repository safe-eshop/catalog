package main

import (
	"catalog/cmd"
	"context"
	"flag"
	"os"

	"github.com/google/subcommands"
	log "github.com/sirupsen/logrus"
)

func main() {
	log.SetFormatter(&log.JSONFormatter{})
	subcommands.Register(&cmd.ImportCatalogProducts{}, "")
	subcommands.Register(&cmd.RunCatalogApi{}, "")
	flag.Parse()
	ctx := context.Background()
	os.Exit(int(subcommands.Execute(ctx)))
}
