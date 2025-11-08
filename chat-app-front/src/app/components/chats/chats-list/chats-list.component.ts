import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { ChatCardComponent } from '../chat-card/chat-card.component';
import { ChatCardModel } from '../../../models/chat-card-model';
import { UserInfoModel } from '../../../models/user-info-model';
import { AuthorizationService } from '../../../services/authorization.service';
import { Emitters } from '../../../emitters/emitters';
import { Router } from '@angular/router';

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
  private router = inject(Router);

  chats: ChatCardModel[] = [
    { id: '1', name: 'dsjnskcnskva', image: './chat-image.jpg' },
    { id: '2', name: 'New super chat', image: './chat-image.jpg' },
    { id: '3', name: 'New super chat', image: './chat-image.jpg' },
    { id: '4', name: 'New super chat', image: './chat-image.jpg' },
    { id: '5', name: 'New super chat', image: './chat-image.jpg' },
    { id: '6', name: 'New super chat', image: './chat-image.jpg' },
    { id: '7', name: 'New super chat', image: './chat-image.jpg' },
    { id: '8', name: 'New super chat', image: './chat-image.jpg' },
    { id: '9', name: 'New super chat', image: './chat-image.jpg' },
    { id: '10', name: 'New super chat', image: './chat-image.jpg' },
    { id: '11', name: 'New super chat', image: './chat-image.jpg' },
  ];

  ngOnInit(): void {
    this.authservice.tryGetCurrentUser().subscribe({
      next: (res) => {
        this.authservice.initUser({
          id: res.id,
          username: res.userName,
          image: './chat-image.jpg',
        });

        console.log(this.authservice.user);
        this.user = this.authservice.user;
      },
      error: (err) => {
        this.router.navigate(['/log-in']);
      },
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
