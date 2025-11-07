import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthorizationService } from './services/authorization.service';
import { Emitters } from './emitters/emitters';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Chat';

  private authService = inject(AuthorizationService);
  private router = inject(Router);

  ngOnInit(): void {
    if (!this.authService.logIn()) {
      this.router.navigate(['log-in']);
    }
  }
}
