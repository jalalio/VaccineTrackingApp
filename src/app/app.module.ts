import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { HttpClientModule } from "@angular/common/http";
import { SharedModule } from "./shared/shared.module";
import { AuthModule } from "./auth/auth.module";
import { HomeComponent } from "./pages/home/home.component";
import { AdminDashboardModule } from "./admin-dashboard/admin-dashboard.module";
import { OrderComponent } from './pages/order/order.component';
import { FormsModule } from "@angular/forms";
import { MatTableModule } from "@angular/material/table";
import { OrderHistoryComponent } from './pages/order-history/order-history.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    OrderComponent,
    OrderHistoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    AuthModule,
    AdminDashboardModule,
    FormsModule,
    MatTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}