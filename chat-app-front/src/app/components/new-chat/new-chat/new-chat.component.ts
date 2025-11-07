import { Component } from '@angular/core';
import { UsersListComponent } from '../users-list/users-list.component';
import { NewChatFormComponent } from '../new-chat-form/new-chat-form.component';

@Component({
  selector: 'app-new-chat',
  standalone: true,
  imports: [UsersListComponent, NewChatFormComponent],
  templateUrl: './new-chat.component.html',
  styleUrl: './new-chat.component.css',
})
export class NewChatComponent {}
