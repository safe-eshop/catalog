package usecases

import (
	"catalog/core/dto"
	"catalog/core/services"
	"context"
	"sync"

	log "github.com/sirupsen/logrus"
)

type ProductImportUseCase interface {
	Execute(ctx context.Context) error
}

type productImportUseCase struct {
	service services.ProductImportService
	bus     services.MessageBus
}

func (uc *productImportUseCase) insertProduct(ctx context.Context, product dto.ProductDto, wg *sync.WaitGroup) {
	defer wg.Done()
	err := uc.service.InsertProduct(ctx, product)
	if err != nil {
		log.WithError(err).Fatalln("Error during product insertion")
	}
	err = uc.bus.Publish(ctx, services.NewProductCreated(product))
	if err != nil {
		log.WithError(err).Fatalln("Message publisher error")
	}

}

func (uc productImportUseCase) Execute(ctx context.Context) error {
	productsStream := uc.service.ProduceProducts(ctx)
	var wg sync.WaitGroup
	for product := range productsStream {
		wg.Add(1)
		go uc.insertProduct(ctx, product, &wg)
	}
	wg.Wait()
	return nil
}

func NewProductImportUseCase(service services.ProductImportService, bus services.MessageBus) ProductImportUseCase {
	return productImportUseCase{service: service, bus: bus}
}
