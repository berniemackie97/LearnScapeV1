import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { faCoffee } from "@fortawesome/free-solid-svg-icons";
import { IPagination } from "./models/pagination";
import { IProduct } from "./models/products";

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
    faCoffee = faCoffee;
    products: IProduct[];

    constructor(private http: HttpClient) {}

    ngOnInit(): void {
        this.http
            .get("https://localhost:5001/api/products?pageSize=50")
            .subscribe(
                (response: IPagination) => {
                    this.products = response.data;
                    console.log(response);
                },
                (error) => {
                    console.log(error);
                }
            );
    }
}
