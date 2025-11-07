import { Component, OnInit } from '@angular/core';
import { UserInfoModel } from '../../../models/user-info-model';
import { SelectedUserCardComponent } from '../selected-user-card/selected-user-card.component';
import { Emitters } from '../../../emitters/emitters';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

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
  selectedUsers: UserInfoModel[] = [];

  OnSubmit() {
    console.log(this.chatName);
    if (!this.validate()) {
      return;
    }
  }

  OnClose() {
    Emitters.addingNewChat.emit(false);
  }

  ngOnInit(): void {
    Emitters.addUserEmitter.subscribe((user) => {
      this.selectedUsers.push(user);
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
    return true;
  }

  OnStartInputName() {
    if (this.isChatNameInvalid) {
      this.isChatNameInvalid = false;
    }
  }
}
