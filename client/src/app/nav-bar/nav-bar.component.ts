import { Component, OnInit } from "@angular/core";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import { faCoffee, faCartShopping } from "@fortawesome/free-solid-svg-icons";

@Component({
    selector: "app-nav-bar",
    templateUrl: "./nav-bar.component.html",
    styleUrls: ["./nav-bar.component.scss"],
})
export class NavBarComponent implements OnInit {
    faCoffee = faCoffee;
    faCartShopping = faCartShopping;
    menuOpen = false;

    constructor() {}

    ngOnInit(): void {}

    toggleMenu() {
        this.menuOpen = !this.menuOpen;
    }
}
