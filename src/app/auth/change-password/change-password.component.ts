
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';
//import { AlertService } from "ngx-alerts";
import { AlertService } from '@full-fledged/alerts';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  urlParams: any = {};

  constructor(private route: ActivatedRoute, private authService: AuthService, public progressBar: ProgressBarService,
    private alertService: AlertService) { }

  ngOnInit() {
    // retrieve token and userid from change password email link
    this.urlParams.token = this.route.snapshot.queryParamMap.get('token');
    this.urlParams.userid = this.route.snapshot.queryParamMap.get('userid');
  }
  
  changePassword() {
    this.alertService.info('Working on changing password');
    this.progressBar.startLoading();
    this.authService.changePassword(this.urlParams).subscribe(() => {
      console.log("Password changed");
      this.progressBar.setSuccess();
      this.alertService.success('Password Changed');
      this.progressBar.completeLoading();
    }, error => {
      console.log('Change password error:');
      console.log(error);
      this.progressBar.setError();
      this.alertService.danger('Unable To Change Password');
      this.progressBar.completeLoading();
    })
  }

}