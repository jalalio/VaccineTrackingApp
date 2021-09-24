import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';
//import { AlertService } from "ngx-alerts";
import { AlertService } from '@full-fledged/alerts';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private authService: AuthService, public progressBar: ProgressBarService,
    private alertService: AlertService, private router: Router) { }

  ngOnInit() {
  }

  onSubmit(f: NgForm) {
    //console.log(f.value);  // { email: '', password: '' }
    //console.log(f.valid);  // false

    this.alertService.info('Working on creating new account');
    this.progressBar.startLoading();
    const registerObserver = {
      next: (x: any) => {
        console.log('User registered');
        this.progressBar.setSuccess();
        this.alertService.success('Account Created');
        this.router.navigate(['login']);
        this.progressBar.completeLoading();
      },
      error: (err: any) => {
        console.log('Register error:');
        console.log(err);
        this.progressBar.setError();
        this.alertService.danger(err.error.errors[0].description);
        this.progressBar.completeLoading();
      }
    };
    this.authService.register(f.value).subscribe(registerObserver);
  }

}