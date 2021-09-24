
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';
//import { AlertService } from "ngx-alerts";
import { AlertService } from '@full-fledged/alerts';
import { Router, RouterLink, RouterLinkWithHref } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService, public progressBar: ProgressBarService,
    private alertService: AlertService, private router: Router) { }

  ngOnInit() {
  }

  onSubmit(f: NgForm) {
    console.log(f.value);  // { email: '', password: '' }
    console.log(f.valid);  // false

    this.alertService.info('Checking Login Info');
    this.progressBar.startLoading();
    const loginObserver = {
      next: (x: any) => {
        console.log('User logged in');
        this.progressBar.setSuccess();
        this.alertService.success('Login Successful');
        this.router.navigate(['']);
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        console.log('Login error:');
        console.log(err);
        this.progressBar.setError();
        this.alertService.danger('Login Failed');
        this.progressBar.completeLoading();
      }
    };
    this.authService.login(f.value).subscribe(loginObserver);

  }

}


