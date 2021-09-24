import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AdminEditComponent } from "./admin-edit/admin-edit.component";

const routes: Routes = [
  {
    path: "admin-edit",
    component: AdminEditComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminDashboardRoutingModule {}