import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { environment } from "src/environments/environment";
import { Basket, IBasketItem, IBasketTotals } from "../shared/models/basket";
import { IProduct } from "../shared/models/products";

@Injectable({
    providedIn: "root",
})
export class BasketService {
    baseUrl = environment.apiUrl;

    private basketSource = new BehaviorSubject<Basket | null>(null);
    basketSource$ = this.basketSource.asObservable();

    private basketTotalSource = new BehaviorSubject<IBasketTotals | null>(null);
    basketTotalSource$ = this.basketTotalSource.asObservable();

    constructor(private http: HttpClient) {}

    getBasket(id: string) {
        return this.http
            .get<Basket>(this.baseUrl + "basket?id=" + id)
            .subscribe({
                next: (basket: Basket) => {
                    this.basketSource.next(basket);
                    this.calculateTotals();
                },
            });
    }

    setBasket(basket: Basket) {
        return this.http
            .post<Basket>(this.baseUrl + "basket", basket)
            .subscribe({
                next: (basket) => {
                    this.basketSource.next(basket);
                    this.calculateTotals();
                },
            });
    }

    getCurrentBasketValue() {
        return this.basketSource.value;
    }

    addItemToBasket(item: IProduct | IBasketItem, quantity = 1) {
        if (this.isProduct(item)) item = this.mapProductItemToBasketItem(item);
        const basket = this.getCurrentBasketValue() ?? this.createBasket();
        basket.items = this.addOrUpdateItem(basket.items, item, quantity);
        this.setBasket(basket);
    }

    removeItemFromBasket(id: number, quantity = 1) {
        const basket = this.getCurrentBasketValue();
        if (!basket) return;
        const item = basket.items.find((i) => i.id === id);
        if (item) {
            item.quantity -= quantity;
            if (item.quantity === 0) {
                basket.items = basket.items.filter((i) => i.id !== id);
            }
            if (basket.items.length > 0) {
                this.setBasket(basket);
            } else {
                this.deleteBasket(basket);
            }
        }
        this.setBasket(basket);
    }
    deleteBasket(basket: Basket) {
        return this.http
            .delete(this.baseUrl + "basket?id=" + basket.id)
            .subscribe({
                next: () => {
                    this.basketSource.next(null);
                    this.basketTotalSource.next(null);
                    localStorage.removeItem("basket_id");
                },
            });
    }

    private addOrUpdateItem(
        items: IBasketItem[],
        itemToAdd: IBasketItem,
        quantity: number
    ): IBasketItem[] {
        const item = items.find((i) => i.id === itemToAdd.id);
        if (item) {
            item.quantity += quantity;
        } else {
            itemToAdd.quantity = quantity;
            items.push(itemToAdd);
        }

        return items;
    }

    private createBasket(): Basket {
        const basket = new Basket();
        localStorage.setItem("basket_id", basket.id);
        return basket;
    }

    private mapProductItemToBasketItem(item: IProduct): IBasketItem {
        return {
            id: item.id,
            productName: item.name,
            price: item.price,
            quantity: 0,
            pictureUrl: item.pictureUrl,
            brand: item.productBrand,
            type: item.productType,
        };
    }

    private calculateTotals() {
        const basket = this.getCurrentBasketValue();
        const shipping = 0;
        const subtotal = basket.items.reduce(
            (previousVal, currentVal) =>
                currentVal.price * currentVal.quantity + previousVal,
            0
        );
        const total = subtotal + shipping;
        this.basketTotalSource.next({ shipping, subtotal, total });
    }

    private isProduct(item: IProduct | IBasketItem): item is IProduct {
        return (item as IProduct).productBrand !== undefined;
    }
}
