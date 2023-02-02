import { Component, OnInit } from "@angular/core";
import { BasketService } from "./basket/basket.service";

export function toggleMobileMenu() {
    console.log("toggleMobileMenu() called");
    const icon = document.getElementById("hamburger-icon");
    console.log(icon.classList);
    icon.classList.toggle("open");
    const menu = document.querySelector(".mobile-menu");
    console.log(menu.classList);
}

@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
    styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
    title = "LearnScape";

    constructor(private basketService: BasketService) {}

    ngOnInit(): void {
        const basket = localStorage.getItem("basket_id");
        if (basket) {
            this.basketService.getBasket(basket);
        }
    }
}
