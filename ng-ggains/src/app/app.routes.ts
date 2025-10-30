import { Routes } from "@angular/router";
import { SigninComponent } from "./signin/signin.component";
import { LandingComponent } from "./landing/landing.component";

export const routes: Routes = [
    { path: "", component: LandingComponent },
    { path: "signin", component: SigninComponent },
];
