import {
  Component,
  inject,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { MessageModel } from '../../../models/message-model';
import { MessageComponent } from '../message/message.component';
import { CommonModule } from '@angular/common';
import { AuthorizationService } from '../../../services/authorization.service';
import { UserInfoModel } from '../../../models/user-info-model';
import { MessagesService } from '../../../services/messages.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-message-list',
  standalone: true,
  imports: [MessageComponent, CommonModule, FormsModule],
  templateUrl: './message-list.component.html',
  styleUrl: './message-list.component.css',
})
export class MessageListComponent implements OnInit, OnChanges {
  @Input({ required: true }) chatId!: string;
  user!: UserInfoModel;

  text: string = '';

  private authService = inject(AuthorizationService);
  private messagesService = inject(MessagesService);

  messages: MessageModel[] = [];

  ngOnInit(): void {
    this.user = this.authService.user;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['chatId'] && this.chatId) {
      this.loadMessages();
    }
  }

  SendMessage() {
    if (this.text.trim() == '') {
      return;
    }
    this.messagesService
      .createMessage({
        chatId: this.chatId,
        text: this.text,
      })
      .subscribe({
        next: (res) => {
          let newMessage = {
            id: res.id,
            text: res.text,
            time: new Date(res.sendDate),
            user: {
              id: res.sender.id,
              username: res.sender.userName,
              image: './chat-image.jpg',
            },
          };
          this.messages.push(newMessage);
          this.text = '';
        },
        error: (err) => {
          console.log(err.error);
        },
      });
  }

  private loadMessages() {
    this.messagesService.getMessagesByChatId(this.chatId).subscribe({
      next: (res) => {
        this.messages = res.map((m) => ({
          id: m.id,
          text: m.text,
          time: new Date(m.sendDate),
          user: {
            id: m.sender.id,
            username: m.sender.userName,
            image: './chat-image.jpg',
          },
        }));
      },
      error: (err) => console.log(err.error),
    });
  }
}
