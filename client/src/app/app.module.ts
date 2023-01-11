import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpClientModule } from "@angular/common/http";
import { AppComponent } from "./app.component";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatIconModule } from "@angular/material/icon";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatListModule } from "@angular/material/list";
import { MatButtonModule } from "@angular/material/button";
import { CoreModule } from "./core/core.module";
import { HomeModule } from "./home/home.module";
import { AppRoutingModule } from "./app-routing.module";
@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        MatToolbarModule,
        MatSidenavModule,
        MatListModule,
        MatButtonModule,
        MatIconModule,
        CoreModule,
        HomeModule,
        AppRoutingModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
