package usecases

import "context"

type ProductImportUseCase interface {
	Execute(ctx context.Context) error
}
