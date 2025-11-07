import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserInfoModel } from '../../../models/user-info-model';
import { Emitters } from '../../../emitters/emitters';

@Component({
  selector: 'app-user-card',
  standalone: true,
  imports: [],
  templateUrl: './user-card.component.html',
  styleUrl: './user-card.component.css',
})
export class UserCardComponent {
  @Input({ required: true }) user!: UserInfoModel;

  @Output() onAddUser = new EventEmitter<string>();

  OnSelectUser() {
    Emitters.addUserEmitter.emit(this.user);
    this.onAddUser.emit(this.user.id);
  }
}
