import { Component } from '@angular/core';
import { RegisterComponent } from '../../components/authorization/register/register.component';
import { LogInComponent } from '../../components/authorization/log-in/log-in.component';

@Component({
  selector: 'app-authorization-page',
  standalone: true,
  imports: [RegisterComponent, LogInComponent],
  templateUrl: './authorization-page.component.html',
  styleUrl: './authorization-page.component.css',
})
export class AuthorizationPageComponent {
  process = 'sing-in';

  OnSingUpChange() {
    this.process = 'sing-up';
  }

  OnSingInChange() {
    this.process = 'sing-in';
  }
}
