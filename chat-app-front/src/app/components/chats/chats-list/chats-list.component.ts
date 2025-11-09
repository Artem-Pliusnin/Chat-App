import {
  Component,
  EventEmitter,
  inject,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { ChatCardComponent } from '../chat-card/chat-card.component';
import { ChatCardModel } from '../../../models/chat-card-model';
import { UserInfoModel } from '../../../models/user-info-model';
import { AuthorizationService } from '../../../services/authorization.service';
import { Emitters } from '../../../emitters/emitters';
import { Router } from '@angular/router';
import { ChatsService } from '../../../services/chats.service';
import { NotificationService } from '../../../services/notification.service';
import { MessagesService } from '../../../services/messages.service';

@Component({
  selector: 'app-chats-list',
  standalone: true,
  imports: [ChatCardComponent],
  templateUrl: './chats-list.component.html',
  styleUrl: './chats-list.component.css',
})
export class ChatsListComponent implements OnInit, OnDestroy {
  selectedChatId?: string;
  user!: UserInfoModel;

  @Output() select = new EventEmitter();

  private authservice = inject(AuthorizationService);
  private chatsservice = inject(ChatsService);
  private messagesService = inject(MessagesService);
  private notificationService = inject(NotificationService);
  private router = inject(Router);

  private addChatSub?: any;
  private newMessageSub?: any;

  chats: ChatCardModel[] = [];

  async ngOnInit() {
    this.authservice.tryGetCurrentUser().subscribe({
      next: (res) => {
        this.authservice.initUser({
          id: res.id,
          username: res.userName,
          image: './chat-image.jpg',
        });
        this.user = this.authservice.user;
        this.GetUserChats();
      },
      error: (err) => {
        this.router.navigate(['/log-in']);
      },
    });

    this.addChatSub = Emitters.addChatEmitter.subscribe((newChat) => {
      this.chats.unshift({
        id: newChat.id,
        name: newChat.name,
        lastmessage: newChat.lastMessage,
        hasUnreadMessages: newChat.hasUnreadMessages,
        image: './chat-image.jpg',
      });
    });

    this.newMessageSub = Emitters.newMessageEmitter.subscribe((message) => {
      const chatIndex = this.chats.findIndex((c) => c.id === message.chatId);
      if (chatIndex !== -1) {
        const chat = this.chats[chatIndex];
        chat.lastmessage = message.text;

        this.chats.splice(chatIndex, 1);
        this.chats.unshift(chat);

        if (message.chatId != this.selectedChatId) {
          this.notificationService.show(message.text, chat.name);
          chat.hasUnreadMessages = true;
        }
      }
    });

    await this.messagesService.connect();
  }

  ngOnDestroy() {
    this.addChatSub?.unsubscribe();
    this.newMessageSub?.unsubscribe();
    this.messagesService.disconnect();
  }

  OnChatClick(id: string) {
    this.selectedChatId = id;
    this.select.emit(this.chats.find((c) => c.id === id));
  }

  OnAddChat() {
    Emitters.addingNewChat.emit(true);
  }

  async LogOut() {
    localStorage.removeItem('jwt');
    await this.messagesService.disconnect();
    this.router.navigate(['/log-in']);
  }

  private GetUserChats() {
    this.chatsservice.getCurrentUserChats().subscribe({
      next: (res) => {
        this.chats = res.map((c) => ({
          id: c.id,
          name: c.name,
          lastmessage: c.lastMessage,
          image: './chat-image.jpg',
          hasUnreadMessages: c.hasUnreadMessages,
        }));
      },
      error: (err) => {
        console.log(err.error);
      },
    });
  }
}
