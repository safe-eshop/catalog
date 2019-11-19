export interface Product {
    readonly id: string
}

export function zero(id: string) {
    return { id: id }
}