import { Injectable } from '@angular/core';
//import { NgProgressRef } from '@ngx-progressbar/core';
import { NgProgressRef } from 'ngx-progressbar';

@Injectable({
  providedIn: 'root'
})
export class ProgressBarService {

  //progressRef: NgProgressRef;
  progressRef!: NgProgressRef; // !
  default: string = "#4c9be8";
  success: string = "#5cb85c";
  error: string = "#d9534f";
  currentColor: string = this.default;

  constructor() { }
  //constructor(public progressRef: NgProgressRef) { } // DI

  startLoading() {
    this.currentColor = this.default;
    this.progressRef.start();
  }

  completeLoading() {
    this.progressRef.complete();
  }

  incLoading(n: number) {
    this.progressRef.inc(n);
  }

  setLoading(n: number) {
    this.progressRef.set(n);
  }

  setSuccess() {
    this.currentColor = this.success;
  }

  setError() {
    this.currentColor = this.error;
  }
}