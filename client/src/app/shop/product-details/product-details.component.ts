import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { faMinusCircle, faPlusCircle } from "@fortawesome/free-solid-svg-icons";
import { BreadcrumbService } from "xng-breadcrumb";
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
        private activatedRoute: ActivatedRoute,
        private bcService: BreadcrumbService
    ) {
        this.bcService.set("@productDetails", " ");
    }

    ngOnInit(): void {
        this.loadProduct();
    }

    loadProduct() {
        this.shopService
            .getProduct(+this.activatedRoute.snapshot.paramMap.get("id"))
            .subscribe(
                (product) => {
                    this.product = product;
                    this.bcService.set("@productDetails", product.name);
                },
                (error) => {
                    console.log(error);
                }
            );
    }
}
