import { Component, inject, OnInit } from '@angular/core';
import {
  NotificationModel,
  NotificationService,
} from '../../../services/notification.service';
import { TruncatePipe } from '../../../pipes/truncate.pipe';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [TruncatePipe],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.css',
})
export class NotificationComponent implements OnInit {
  notifications: NotificationModel[] = [];

  private notificationService = inject(NotificationService);

  ngOnInit(): void {
    this.notificationService.notifications$.subscribe((n) => {
      this.notifications.push(n);
      setTimeout(() => this.removeNotification(n), 4000);
    });
  }

  removeNotification(n: NotificationModel) {
    this.notifications = this.notifications.filter((x) => x !== n);
  }
}
