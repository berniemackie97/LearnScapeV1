import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { take } from "rxjs";
import { BasketService } from "src/app/basket/basket.service";
import { BreadcrumbService } from "xng-breadcrumb";
import { IProduct } from "../../shared/models/products";
import { ShopService } from "../shop.service";

@Component({
    selector: "app-product-details",
    templateUrl: "./product-details.component.html",
    styleUrls: ["./product-details.component.scss"],
})
export class ProductDetailsComponent implements OnInit {
    product?: IProduct;
    quantity = 1;
    quantityInBasket = 0;

    constructor(
        private shopService: ShopService,
        private activatedRoute: ActivatedRoute,
        private bcService: BreadcrumbService,
        private basketService: BasketService
    ) {
        this.bcService.set("@productDetails", " ");
    }

    ngOnInit(): void {
        this.loadProduct();
    }

    loadProduct() {
        const id = this.activatedRoute.snapshot.paramMap.get("id");
        if (id)
            this.shopService.getProduct(+id).subscribe({
                next: (product) => {
                    this.product = product;
                    this.bcService.set("@productDetails", product.name);
                    this.basketService.basketSource$.pipe(take(1)).subscribe({
                        next: (basket) => {
                            const item = basket?.items.find(
                                (i) => i.id === +id
                            );
                            if (item) {
                                this.quantity = item.quantity;
                                this.quantityInBasket = item.quantity;
                            }
                        },
                    });
                },
                error: (error) => console.log(error),
            });
    }

    incrementQuantity() {
        this.quantity++;
    }

    decrementQuantity() {
        if (this.quantity > 0 && this.quantityInBasket != 0) this.quantity--;
    }

    updateBasket() {
        if (this.product) {
            if (this.quantity > this.quantityInBasket) {
                const itemsToAdd = this.quantity - this.quantityInBasket;
                this.quantityInBasket += itemsToAdd;
                this.basketService.addItemToBasket(this.product, itemsToAdd);
            } else {
                const itemsToRemove = this.quantityInBasket - this.quantity;
                this.quantityInBasket -= itemsToRemove;
                this.basketService.removeItemFromBasket(
                    this.product.id,
                    itemsToRemove
                );
            }
        }
    }

    get buttonText() {
        return this.quantityInBasket > 0 ? "Update Basket" : "Add to Basket";
    }
}
