import { Component, OnInit } from "@angular/core";
import { faCoffee, faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { BasketService } from "src/app/basket/basket.service";
import { IBasketItem } from "src/app/shared/models/basket";

@Component({
    selector: "app-nav-bar",
    templateUrl: "./nav-bar.component.html",
    styleUrls: ["./nav-bar.component.scss"],
})
export class NavBarComponent implements OnInit {
    faCoffee = faCoffee;
    faCartShopping = faCartShopping;

    constructor(public basketService: BasketService) {}

    getCount(items: IBasketItem[]) {
        return items.reduce((sum, item) => sum + item.quantity, 0);
    }

    ngOnInit(): void {}
}
