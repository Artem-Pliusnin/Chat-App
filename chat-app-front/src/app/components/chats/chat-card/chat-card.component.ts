import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ChatCardModel } from '../../../models/chat-card-model';

@Component({
  selector: 'app-chat-card',
  standalone: true,
  imports: [],
  templateUrl: './chat-card.component.html',
  styleUrl: './chat-card.component.css',
})
export class ChatCardComponent {
  @Input({ required: true }) chat!: ChatCardModel;
  @Input({ required: true }) selected!: boolean;
  @Output() select = new EventEmitter();

  onCardClick() {
    this.select.emit(this.chat.id);
  }
}
