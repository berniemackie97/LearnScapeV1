import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import { IBrand } from "../shared/models/brand";
import { IProduct } from "../shared/models/products";
import { IProductType } from "../shared/models/productType";
import { ShopParams } from "../shared/helpers/shopParams";
import { ShopService } from "./shop.service";

@Component({
    selector: "app-shop",
    templateUrl: "./shop.component.html",
    styleUrls: ["./shop.component.scss"],
})
export class ShopComponent implements OnInit {
    @ViewChild("search", { static: false }) searchTerm: ElementRef;
    products: IProduct[];
    brands: IBrand[];
    productTypes: IProductType[];
    shopParams = new ShopParams();
    totalCount: number;
    sortOptions = [
        { name: "Alphabetical", value: "name" },
        { name: "Price: Low to High", value: "priceAsc" },
        { name: "Price: High to Low", value: "priceDesc" },
    ];

    constructor(private shopService: ShopService) {}

    ngOnInit(): void {
        this.getProducts();
        this.getBrands();
        this.getProductTypes();
    }

    getProducts() {
        this.shopService.getProducts(this.shopParams).subscribe({
            next: (response) => {
                this.products = response.data;
                this.shopParams.pageNumber = response.pageIndex;
                this.shopParams.pageSize = response.pageSize;
                this.totalCount = response.count;
            },
            error: (error) => console.log(error),
        });
    }

    getBrands() {
        this.shopService.getBrands().subscribe({
            next: (response) => {
                this.brands = [{ id: 0, name: "All" }, ...response];
            },
            error: (error) => console.log(error),
        });
    }

    getProductTypes() {
        this.shopService.getProductTypes().subscribe({
            next: (response) => {
                this.productTypes = [{ id: 0, name: "All" }, ...response];
            },
            error: (error) => console.log(error),
        });
    }

    onBrandSelected(brandId: number) {
        this.shopParams.brandId = brandId;
        this.shopParams.pageNumber = 1;
        this.getProducts();
    }

    onProductTypeSelected(productTypeId: number) {
        this.shopParams.productTypeId = productTypeId;
        this.shopParams.pageNumber = 1;

        this.getProducts();
    }

    onSortSelected(sort: string) {
        this.shopParams.sort = sort;
        this.getProducts();
    }

    onPageChanged(event: any) {
        if (this.shopParams.pageNumber !== event) {
            this.shopParams.pageNumber = event;
            this.getProducts();
        }
    }

    onSearch() {
        this.shopParams.search = this.searchTerm.nativeElement.value;
        this.shopParams.pageNumber = 1;
        this.getProducts();
    }

    onReset() {
        this.searchTerm.nativeElement.value = "";
        this.shopParams = new ShopParams();
        this.getProducts();
    }
}
