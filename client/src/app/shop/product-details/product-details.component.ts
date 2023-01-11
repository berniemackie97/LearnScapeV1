import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { faMinusCircle, faPlusCircle } from "@fortawesome/free-solid-svg-icons";
import { IProduct } from "../../shared/models/products";
import { ShopService } from "../shop.service";

@Component({
    selector: "app-product-details",
    templateUrl: "./product-details.component.html",
    styleUrls: ["./product-details.component.scss"],
})
export class ProductDetailsComponent implements OnInit {
    product: IProduct;
    faMinusCircle = faMinusCircle;
    faPlusCircle = faPlusCircle;

    constructor(
        private shopService: ShopService,
        private activatedRoute: ActivatedRoute
    ) {}

    ngOnInit(): void {
        this.loadProduct();
    }

    loadProduct() {
        this.shopService
            .getProduct(+this.activatedRoute.snapshot.paramMap.get("id"))
            .subscribe(
                (product) => {
                    this.product = product;
                },
                (error) => {
                    console.log(error);
                }
            );
    }
}
