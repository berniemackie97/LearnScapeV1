import { Component } from "@angular/core";
import * as $ from "jquery";
import {
    faHomeUser,
    faBars,
    faSliders,
    faCampground,
    faSignIn,
    faUserPlus,
    faBook,
} from "@fortawesome/free-solid-svg-icons";

@Component({
    selector: "app-nav-bar",
    templateUrl: "./nav-bar.component.html",
    styleUrls: ["./nav-bar.component.scss"],
})
export class NavBarComponent {
    faHomeUser = faHomeUser;
    faBars = faBars;
    faSliders = faSliders;
    faBook = faBook;
    faCampground = faCampground;
    faSignIn = faSignIn;
    faUserPlus = faUserPlus;
}

// ---------Responsive-navbar-active-animation-----------
function test() {
    const tabsNewAnim = $("#navbarSupportedContent");
    const selectorNewAnim = $("#navbarSupportedContent").find("li").length;
    const activeItemNewAnim = tabsNewAnim.find(".active");
    const activeWidthNewAnimHeight = activeItemNewAnim.innerHeight();
    const activeWidthNewAnimWidth = activeItemNewAnim.innerWidth();
    const itemPosNewAnimTop = activeItemNewAnim.position();
    const itemPosNewAnimLeft = activeItemNewAnim.position();
    $(".hori-selector").css({
        top: itemPosNewAnimTop.top + "px",
        left: itemPosNewAnimLeft.left + "px",
        height: activeWidthNewAnimHeight + "px",
        width: activeWidthNewAnimWidth + "px",
    });
    $("#navbarSupportedContent").on("click", "li", function (e) {
        $("#navbarSupportedContent ul li").removeClass("active");
        $(this).addClass("active");
        const activeWidthNewAnimHeight = $(this).innerHeight();
        const activeWidthNewAnimWidth = $(this).innerWidth();
        const itemPosNewAnimTop = $(this).position();
        const itemPosNewAnimLeft = $(this).position();
        $(".hori-selector").css({
            top: itemPosNewAnimTop.top + "px",
            left: itemPosNewAnimLeft.left + "px",
            height: activeWidthNewAnimHeight + "px",
            width: activeWidthNewAnimWidth + "px",
        });
    });
}
$(document).ready(function () {
    setTimeout(function () {
        test();
    });
});
$(window).on("resize", function () {
    setTimeout(function () {
        test();
    }, 500);
});
$(".navbar-toggler").click(function () {
    $(".navbar-collapse").slideToggle(300);
    setTimeout(function () {
        test();
    });
});

// --------------add active class-on another-page move----------
$(document).ready(function ($) {
    // Get current path and find target link
    let path = window.location.pathname.split("/").pop();

    // Account for home page with empty path
    if (path == "") {
        path = "index.html";
    }

    const target = $('#navbarSupportedContent ul li a[href="' + path + '"]');
    // Add active class to target link
    target.parent().addClass("active");
});

//Add active class on another page linked
//==========================================
//$(window).on("load", function () {
//    const current = location.pathname;
//    console.log(current);
//    $("#navbarSupportedContent ul li a").each(function () {
//        const $this = $(this);
//        // if the current path is like this link, make it active
//        if ($this.attr("href").indexOf(current) !== -1) {
//            $this.parent().addClass("active");
//            $this.parents(".menu-submenu").addClass("show-dropdown");
//            $this.parents(".menu-submenu").parent().addClass("active");
//        } else {
//            $this.parent().removeClass("active");
//        }
//    });
//});
