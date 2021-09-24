
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';
//import { AlertService } from "ngx-alerts";
import { AlertService } from '@full-fledged/alerts';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  constructor(private authService: AuthService, public progressBar: ProgressBarService,
    private alertService: AlertService) { }

  ngOnInit() {
  }

  onSubmit(f: NgForm) {
    //console.log(f.value);  // { email: '' }
    //console.log(f.valid);  // false

    this.alertService.info('Working on sending email');
    this.progressBar.startLoading();
    const resetPasswordObserver = {
      next: (x: any) => {
        console.log('Email to change password sent');
        this.progressBar.setSuccess();
        this.alertService.success('Reset Password Email Sent');
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        console.log('Email to change password not sent');
        console.log(err);
        this.progressBar.setError();
        this.alertService.danger('Unable To Send Email');
        this.progressBar.completeLoading();
      }
    };
    this.authService.resetPassword(f.value).subscribe(resetPasswordObserver);
  }

}