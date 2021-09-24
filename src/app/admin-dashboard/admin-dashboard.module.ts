import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { AdminDashboardRoutingModule } from "./admin-dashboard-routing.module";
import { AdminEditComponent } from "./admin-edit/admin-edit.component";
import { FormsModule } from "@angular/forms";

@NgModule({
  declarations: [
    AdminEditComponent
  ],
  imports: [
    CommonModule, 
    AdminDashboardRoutingModule, 
    FormsModule
  ],
  exports: [AdminEditComponent]
})
export class AdminDashboardModule {}