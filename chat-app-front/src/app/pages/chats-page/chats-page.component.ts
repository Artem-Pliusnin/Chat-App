import { Component, OnInit } from '@angular/core';
import { ChatsListComponent } from '../../components/chats/chats-list/chats-list.component';
import { ChatComponent } from '../../components/chats/chat/chat.component';
import { ChatCardModel } from '../../models/chat-card-model';
import { Emitters } from '../../emitters/emitters';
import { NewChatComponent } from '../../components/new-chat/new-chat/new-chat.component';

@Component({
  selector: 'app-chats-page',
  standalone: true,
  imports: [ChatsListComponent, ChatComponent, NewChatComponent],
  templateUrl: './chats-page.component.html',
  styleUrl: './chats-page.component.css',
})
export class ChatsPageComponent implements OnInit {
  selectedChat!: ChatCardModel;
  isAddingNewChat: boolean = false;

  OnChatSelect(chat: ChatCardModel) {
    this.selectedChat = chat;
  }

  ngOnInit(): void {
    Emitters.addingNewChat.subscribe((value) => {
      console.log(value);
      this.isAddingNewChat = value;
    });
  }
}
