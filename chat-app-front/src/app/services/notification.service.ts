import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export type NotificationModel = {
  message: string;
  chat: string;
};

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor() {}
  private notificationSubject = new Subject<NotificationModel>();
  public notifications$ = this.notificationSubject.asObservable();

  show(message: string, chat: string) {
    this.notificationSubject.next({ message, chat });
  }
}
