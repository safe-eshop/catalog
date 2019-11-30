import {Product} from "../../domain/model/product";
import {IProductRepository} from "../../domain/repository/product";
import uuid from "uuid/v4"

export async function seedDatabaseUseCase(repo: IProductRepository) {
    const count = await repo.count();
    if (count === 0) {
        const products: Product[] = [{
            price: 1.0,
            info: {brand: "test", description: "super product", name: "Shampoo", slug: "1", picture: "adsdas" },
            details: { color: "Red", manufacturer: "Super producent", weight: 1, weightUnits: "kg"},
            id: uuid()
        }];
        await repo.insertMany(products);   
    }
    console.log("Database seeded")
}
