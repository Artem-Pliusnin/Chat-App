import { Component, OnInit } from '@angular/core';
import { UserCardComponent } from '../user-card/user-card.component';
import { UserInfoModel } from '../../../models/user-info-model';
import { Emitters } from '../../../emitters/emitters';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [UserCardComponent],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css',
})
export class UsersListComponent implements OnInit {
  users: UserInfoModel[] = [
    { id: '1', username: 'dsjnskcnskva', image: './chat-image.jpg' },
    { id: '2', username: 'New super chat', image: './chat-image.jpg' },
    { id: '3', username: 'Artem', image: './chat-image.jpg' },
    { id: '4', username: 'New super chat', image: './chat-image.jpg' },
    { id: '5', username: 'Artem Pliusnin', image: './chat-image.jpg' },
    { id: '6', username: 'New super chat', image: './chat-image.jpg' },
    { id: '7', username: 'Artem', image: './chat-image.jpg' },
    { id: '8', username: 'Artem Pliusnin', image: './chat-image.jpg' },
    { id: '9', username: 'New super chat', image: './chat-image.jpg' },
    { id: '10', username: 'Artem Pliusnin', image: './chat-image.jpg' },
    { id: '11', username: 'New super chat', image: './chat-image.jpg' },
  ];

  selectedUsers: string[] = [];

  onSelectUser(id: string) {
    this.users = this.users.filter((u) => u.id != id);
    this.selectedUsers.push(id);
  }

  ngOnInit(): void {
    Emitters.delteUserEmitter.subscribe((user) => {
      this.selectedUsers = this.selectedUsers.filter((e) => e != user.id);
    });
  }
}
