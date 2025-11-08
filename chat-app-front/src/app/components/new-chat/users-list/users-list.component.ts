import { Component, inject, OnInit } from '@angular/core';
import { UserCardComponent } from '../user-card/user-card.component';
import { UserInfoModel } from '../../../models/user-info-model';
import { Emitters } from '../../../emitters/emitters';
import { UsersService } from '../../../services/users.service';
import { AuthorizationService } from '../../../services/authorization.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [UserCardComponent, FormsModule],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css',
})
export class UsersListComponent implements OnInit {
  username: string = '';
  users: UserInfoModel[] = [];

  selectedUsers: string[] = [];

  private usersService = inject(UsersService);
  private authService = inject(AuthorizationService);

  onSelectUser(id: string) {
    this.users = this.users.filter((u) => u.id != id);
    this.selectedUsers.push(id);
  }

  onFind() {
    const excludeIds = [...this.selectedUsers, this.authService.user.id];
    this.usersService.searchUsers(this.username, excludeIds).subscribe({
      next: (res) => {
        this.users = res.map((u) => ({
          id: u.id,
          username: u.userName,
          image: './chat-image.jpg',
        }));
      },
      error: (err) => {
        console.log(err.error);
      },
    });
  }

  ngOnInit(): void {
    const excludeIds = [...this.selectedUsers, this.authService.user.id];
    this.usersService.searchUsers(this.username, excludeIds).subscribe({
      next: (res) => {
        this.users = res.map((u) => ({
          id: u.id,
          username: u.userName,
          image: './chat-image.jpg',
        }));
      },
      error: (err) => {
        console.log(err.error);
      },
    });
    Emitters.delteUserEmitter.subscribe((user) => {
      this.selectedUsers = this.selectedUsers.filter((e) => e != user.id);
      this.onFind();
    });
  }
}
