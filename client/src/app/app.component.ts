import { Component, OnInit } from "@angular/core";
import { faCoffee, faMinusCircle, faPlusCircle } from "@fortawesome/free-solid-svg-icons";

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


    constructor() {}

    ngOnInit(): void {}
}
