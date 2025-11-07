import { Component, Input } from '@angular/core';
import { ChatCardModel } from '../../../models/chat-card-model';

@Component({
  selector: 'app-chat-header',
  standalone: true,
  imports: [],
  templateUrl: './chat-header.component.html',
  styleUrl: './chat-header.component.css',
})
export class ChatHeaderComponent {
  @Input({ required: true }) chat!: ChatCardModel;
}
