import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ShopComponent } from "./shop.component";
import { ProductItemComponent } from "./product-item/product-item.component";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { SharedModule } from "../shared/shared.module";
import { PagingHeaderComponent } from "../shared/components/paging-header/paging-header.component";

@NgModule({
    declarations: [ShopComponent, ProductItemComponent],
    imports: [CommonModule, FontAwesomeModule, SharedModule],
    exports: [ShopComponent],
})
export class ShopModule {}
