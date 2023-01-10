import { Component, OnInit } from "@angular/core";
import { faCoffee, faCartShopping } from "@fortawesome/free-solid-svg-icons";

@Component({
    selector: "app-nav-bar",
    templateUrl: "./nav-bar.component.html",
    styleUrls: ["./nav-bar.component.scss"],
})
export class NavBarComponent implements OnInit {
    faCoffee = faCoffee;
    faCartShopping = faCartShopping;

    constructor() {}

    ngOnInit(): void {}
}
