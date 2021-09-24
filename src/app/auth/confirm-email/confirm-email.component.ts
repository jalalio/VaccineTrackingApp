
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';
//import { AlertService } from "ngx-alerts";
import { AlertService } from '@full-fledged/alerts';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  emailConfirmed: boolean = false;
  urlParams: any = {};

  constructor(private route: ActivatedRoute, private authService: AuthService, public progressBar: ProgressBarService,
    private alertService: AlertService) { }

  ngOnInit() {
    // retrieve token and userid from confirmation email link
    this.urlParams.token = this.route.snapshot.queryParamMap.get('token');
    this.urlParams.userid = this.route.snapshot.queryParamMap.get('userid');
    this.confirmEmail();
  }

  confirmEmail() {
    this.progressBar.startLoading();
    this.authService.confirmEmail(this.urlParams).subscribe(() => {
      console.log("User confirmed new account");
      this.progressBar.setSuccess();
      this.alertService.success('Email Confirmed');
      this.progressBar.completeLoading();
      this.emailConfirmed = true;
    }, error => {
      console.log('Confirm email error:');
      console.log(error);
      this.progressBar.setError();
      this.alertService.danger('Unable to confirm email');
      this.progressBar.completeLoading();
      this.emailConfirmed = false;
    })
  }

}