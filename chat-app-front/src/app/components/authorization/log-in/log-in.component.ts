import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.css',
})
export class LogInComponent {
  email: string = '';
  password: string = '';

  errormessage = '';
  isEmailInvalid = false;
  isPasswordInvalid = false;

  OnSubmit() {
    if (!this.validate()) {
      return;
    }
  }

  validate(): boolean {
    this.isEmailInvalid = this.email == '';
    this.isPasswordInvalid = this.password == '';

    if (this.isEmailInvalid || this.isPasswordInvalid) {
      this.errormessage = 'All fields are required';
      return false;
    }

    const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (!emailPattern.test(this.email)) {
      this.errormessage = 'Invalid email address';
      this.isEmailInvalid = true;
      return false;
    }

    return true;
  }

  OnStartInputPassword() {
    if (this.isPasswordInvalid) {
      this.isPasswordInvalid = false;
      if (!this.isEmailInvalid && !this.isPasswordInvalid) {
        this.errormessage = '';
      }
    }
  }

  OnStartInputEmail() {
    if (this.isEmailInvalid) {
      this.isEmailInvalid = false;
      if (!this.isEmailInvalid && !this.isPasswordInvalid) {
        this.errormessage = '';
      }
    }
  }
}
