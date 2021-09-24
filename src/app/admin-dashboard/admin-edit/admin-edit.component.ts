
import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/shared/services/auth.service";
import { ProgressBarService } from "src/app/shared/services/progress-bar.service";
import { AlertService } from "ngx-alerts";
import { AdminService } from "src/app/shared/services/admin.service";

@Component({
  selector: "app-Admin-edit",
  templateUrl: "./Admin-edit.component.html",
  styleUrls: ["./Admin-edit.component.scss"]
})
export class AdminEditComponent implements OnInit {
  model: any = {};

  constructor(
    public authService: AuthService,
    public progressBar: ProgressBarService,
    private alertService: AlertService,
    private adminService: AdminService
  ) {}

  ngOnInit() {}

  onFileChange(e: any) {
    this.model.file = e.target.files[0];
  }

  onSubmit() {
    this.alertService.info("Updating Account");
    this.progressBar.startLoading();
    const updateAdminObserver = {
      next: (x: any) => {
        this.progressBar.setSuccess();
        console.log("Account Updated");
        this.alertService.success("Account Updated");
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        this.progressBar.setError();
        console.log(err);
        this.alertService.danger("Unable to Update Account");
        this.progressBar.completeLoading();
      }
    };
    this.adminService
      .updateAdmin(this.model)
      .subscribe(updateAdminObserver);
  }
}