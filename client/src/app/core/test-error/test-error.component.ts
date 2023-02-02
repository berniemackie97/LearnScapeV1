import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { environment } from "src/environments/environment";

@Component({
    selector: "app-test-error",
    templateUrl: "./test-error.component.html",
    styleUrls: ["./test-error.component.scss"],
})
export class TestErrorComponent implements OnInit {
    baseUrl = environment.apiUrl;
    validationErrors: any;

    constructor(private http: HttpClient) {}

    ngOnInit() {}

    debugger;
    get404Error() {
        this.http.get("https://localhost:5001/notfound").subscribe({
            next: (response) => console.log(response),
            error: (error) => console.log(error),
        });
    }

    get500Error() {
        this.http.get("https://localhost:5001/servererror").subscribe({
            next: (response) => console.log(response),
            error: (error) => console.log(error),
        });
    }

    get400Error() {
        this.http.get("https://localhost:5001/badrequest").subscribe({
            next: (response) => console.log(response),
            error: (error) => console.log(error),
        });
    }

    get400ValidationError() {
        this.http
            .get("https://localhost:5001/api/products/fortytwop")
            .subscribe({
                next: (response) => console.log(response),
                error: (error) => {
                    console.log(error);
                    this.validationErrors = error.errors;
                },
            });
    }
}
