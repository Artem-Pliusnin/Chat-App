import { Component, inject, OnInit } from '@angular/core';
import { UserInfoModel } from '../../../models/user-info-model';
import { SelectedUserCardComponent } from '../selected-user-card/selected-user-card.component';
import { Emitters } from '../../../emitters/emitters';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthorizationService } from '../../../services/authorization.service';
import { ChatsService } from '../../../services/chats.service';
import { ChatCardModel } from '../../../models/chat-card-model';

@Component({
  selector: 'app-new-chat-form',
  standalone: true,
  imports: [SelectedUserCardComponent, CommonModule, FormsModule],
  templateUrl: './new-chat-form.component.html',
  styleUrl: './new-chat-form.component.css',
})
export class NewChatFormComponent implements OnInit {
  chatName: string = '';
  isChatNameInvalid = false;
  isNotEnoufUsers = false;
  selectedUsers: UserInfoModel[] = [];

  private authService = inject(AuthorizationService);
  private chatsService = inject(ChatsService);

  OnSubmit() {
    if (!this.validate()) {
      return;
    }

    let members = this.selectedUsers.map((u) => u.id);
    this.chatsService
      .createChat({ name: this.chatName, memberIds: members })
      .subscribe({
        next: (res) => {
          let newChat: ChatCardModel = {
            id: res.id,
            name: res.name,
            lastmessage: res.lastMessage,
            image: './chat-image.jpg',
          };
          Emitters.addChatEmitter.emit(newChat);
          Emitters.addingNewChat.emit(false);
        },
        error: (err) => {
          console.log(err.error);
        },
      });
  }

  OnClose() {
    Emitters.addingNewChat.emit(false);
  }

  ngOnInit(): void {
    Emitters.addUserEmitter.subscribe((user) => {
      this.selectedUsers.push(user);
      this.onAddUser();
    });

    Emitters.delteUserEmitter.subscribe((user) => {
      this.selectedUsers = this.selectedUsers.filter((u) => u.id != user.id);
    });
  }

  validate(): boolean {
    if (this.chatName == '') {
      this.isChatNameInvalid = true;
      return false;
    }
    if (this.selectedUsers.length < 1) {
      this.isNotEnoufUsers = true;
      return false;
    }
    return true;
  }

  OnStartInputName() {
    if (this.isChatNameInvalid) {
      this.isChatNameInvalid = false;
    }
  }

  private onAddUser() {
    if (this.isNotEnoufUsers) {
      this.isNotEnoufUsers = false;
    }
  }
}
