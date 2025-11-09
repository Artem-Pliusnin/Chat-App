import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { ChatCardComponent } from '../chat-card/chat-card.component';
import { ChatCardModel } from '../../../models/chat-card-model';
import { UserInfoModel } from '../../../models/user-info-model';
import { AuthorizationService } from '../../../services/authorization.service';
import { Emitters } from '../../../emitters/emitters';
import { Router } from '@angular/router';
import { ChatsService } from '../../../services/chats.service';

@Component({
  selector: 'app-chats-list',
  standalone: true,
  imports: [ChatCardComponent],
  templateUrl: './chats-list.component.html',
  styleUrl: './chats-list.component.css',
})
export class ChatsListComponent implements OnInit {
  selectedChatId?: string;
  user!: UserInfoModel;

  @Output() select = new EventEmitter();

  private authservice = inject(AuthorizationService);
  private chatsservice = inject(ChatsService);
  private router = inject(Router);

  chats: ChatCardModel[] = [];

  ngOnInit(): void {
    this.authservice.tryGetCurrentUser().subscribe({
      next: (res) => {
        this.authservice.initUser({
          id: res.id,
          username: res.userName,
          image: './chat-image.jpg',
        });
        this.user = this.authservice.user;

        this.chatsservice.getCurrentUserChats().subscribe({
          next: (res) => {
            this.chats = res.map((c) => ({
              id: c.id,
              name: c.name,
              lastmessage: c.lastMessage,
              image: './chat-image.jpg',
            }));
          },
          error: (err) => {
            console.log(err.error);
          },
        });
      },
      error: (err) => {
        this.router.navigate(['/log-in']);
      },
    });

    Emitters.addChatEmitter.subscribe((chat) => {
      this.chats.unshift(chat);
    });

    Emitters.newMessageEmitter.subscribe((message) => {
      if (message.chatId != this.selectedChatId) {
        alert(message.sentiment);
        console.log(message);
      }
      const chatIndex = this.chats.findIndex((c) => c.id === message.chatId);
      if (chatIndex !== -1) {
        const chat = this.chats[chatIndex];
        chat.lastmessage = message.text;

        this.chats.splice(chatIndex, 1);
        this.chats.unshift(chat);
      }
    });
  }

  OnChatClick(id: string) {
    this.selectedChatId = id;
    this.select.emit(this.chats.find((c) => c.id === id));
  }

  OnAddChat() {
    Emitters.addingNewChat.emit(true);
  }

  LogOut() {
    localStorage.removeItem('jwt');
    this.router.navigate(['/log-in']);
  }
}
