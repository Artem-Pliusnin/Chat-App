import { Component, Input } from '@angular/core';
import { ChatHeaderComponent } from '../chat-header/chat-header.component';
import { ChatCardModel } from '../../../models/chat-card-model';
import { MessageListComponent } from '../message-list/message-list.component';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [ChatHeaderComponent, MessageListComponent],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent {
  @Input({ required: true }) chat!: ChatCardModel;
}
