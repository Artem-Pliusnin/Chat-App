import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthorizationService } from '../../../services/authorization.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  username: string = '';
  email: string = '';
  password: string = '';
  repeatPassword: string = '';

  errormessage = '';
  isEmailInvalid = false;
  isPasswordInvalid = false;
  isUsernameInvalid = false;
  isRepeatPasswordInvalid = false;

  private authService = inject(AuthorizationService);

  @Output() toSignIn = new EventEmitter();

  OnSubmit() {
    if (!this.validate()) {
      return;
    }

    this.authService
      .signUp({
        username: this.username,
        email: this.email,
        password: this.password,
      })
      .subscribe({
        next: (res) => {
          this.toSignIn.emit();
        },
        error: (err) => (this.errormessage = err.error.message),
      });
  }

  validate(): boolean {
    this.isUsernameInvalid = this.username == '';
    this.isEmailInvalid = this.email == '';
    this.isPasswordInvalid = this.password == '';
    this.isRepeatPasswordInvalid = this.repeatPassword == '';

    if (
      this.isUsernameInvalid ||
      this.isEmailInvalid ||
      this.isPasswordInvalid ||
      this.isRepeatPasswordInvalid
    ) {
      this.errormessage = 'All fields are required';
      return false;
    }

    const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (!emailPattern.test(this.email)) {
      this.errormessage = 'Invalid email address';
      this.isEmailInvalid = true;
      return false;
    }

    if (this.password !== this.repeatPassword) {
      this.errormessage = 'Passwords do not match';
      this.isRepeatPasswordInvalid = true;
      return false;
    }

    return true;
  }

  OnStartInputUsername() {
    if (this.isUsernameInvalid) {
      this.isUsernameInvalid = false;
      if (
        !this.isUsernameInvalid &&
        !this.isEmailInvalid &&
        !this.isPasswordInvalid &&
        !this.isRepeatPasswordInvalid
      ) {
        this.errormessage = '';
      }
    }
  }

  OnStartInputEmail() {
    if (this.isEmailInvalid) {
      this.isEmailInvalid = false;
      if (
        !this.isUsernameInvalid &&
        !this.isEmailInvalid &&
        !this.isPasswordInvalid &&
        !this.isRepeatPasswordInvalid
      ) {
        this.errormessage = '';
      }
    }
  }

  OnStartInputPassword() {
    if (this.isPasswordInvalid) {
      this.isPasswordInvalid = false;
      if (
        !this.isUsernameInvalid &&
        !this.isEmailInvalid &&
        !this.isPasswordInvalid &&
        !this.isRepeatPasswordInvalid
      ) {
        this.errormessage = '';
      }
    }
  }

  OnStartInputRepeatedPassword() {
    if (this.isRepeatPasswordInvalid) {
      this.isRepeatPasswordInvalid = false;
      if (
        !this.isUsernameInvalid &&
        !this.isEmailInvalid &&
        !this.isPasswordInvalid &&
        !this.isRepeatPasswordInvalid
      ) {
        this.errormessage = '';
      }
    }
  }
}
