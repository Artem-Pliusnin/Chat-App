import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { ChatsListComponent } from '../../components/chats/chats-list/chats-list.component';
import { ChatComponent } from '../../components/chats/chat/chat.component';
import { ChatCardModel } from '../../models/chat-card-model';
import { Emitters } from '../../emitters/emitters';
import { NewChatComponent } from '../../components/new-chat/new-chat/new-chat.component';
import { MessagesService } from '../../services/messages.service';

@Component({
  selector: 'app-chats-page',
  standalone: true,
  imports: [ChatsListComponent, ChatComponent, NewChatComponent],
  templateUrl: './chats-page.component.html',
  styleUrl: './chats-page.component.css',
})
export class ChatsPageComponent implements OnInit, OnDestroy {
  selectedChat!: ChatCardModel;
  isAddingNewChat: boolean = false;

  private messagesService = inject(MessagesService);

  OnChatSelect(chat: ChatCardModel) {
    this.selectedChat = chat;
  }

  async ngOnInit() {
    Emitters.addingNewChat.subscribe((value) => {
      console.log(value);
      this.isAddingNewChat = value;
    });

    await this.messagesService.connect();
  }

  async ngOnDestroy() {
    await this.messagesService.disconnect();
  }
}
