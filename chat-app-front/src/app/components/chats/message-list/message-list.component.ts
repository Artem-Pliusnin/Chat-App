import { Component, inject, Input, OnInit } from '@angular/core';
import { MessageModel } from '../../../models/message-model';
import { MessageComponent } from '../message/message.component';
import { CommonModule } from '@angular/common';
import { AuthorizationService } from '../../../services/authorization.service';
import { UserInfoModel } from '../../../models/user-info-model';

@Component({
  selector: 'app-message-list',
  standalone: true,
  imports: [MessageComponent, CommonModule],
  templateUrl: './message-list.component.html',
  styleUrl: './message-list.component.css',
})
export class MessageListComponent implements OnInit {
  @Input({ required: true }) chatId!: string;
  user!: UserInfoModel;

  private authService = inject(AuthorizationService);

  messages: MessageModel[] = [
    {
      id: '1',
      text: 'Hi my name is artem',
      time: new Date(),
      user: {
        id: '1',
        username: 'Artem Pliusnin',
        image: './chat-image.jpg',
      },
    },
    {
      id: '2',
      text: 'Hi',
      time: new Date(),
      user: {
        id: '2',
        username: 'Artem Pliusnin',
        image: './chat-image.jpg',
      },
    },
    {
      id: '1',
      text: 'Hi my name is artem Hi my name is artem Hi my name is artem Hi my name is artem Hi my name is artem Hi my name is artem Hi my name is artem',
      time: new Date(),
      user: {
        id: '2',
        username: 'Artem Pliusnin',
        image: './chat-image.jpg',
      },
    },
    {
      id: '2',
      text: 'Hi my name is artem Hi my name is artem Hi my name is artem',
      time: new Date(),
      user: {
        id: '1',
        username: 'Artem Pliusnin',
        image: './chat-image.jpg',
      },
    },
    {
      id: '1',
      text: 'Hi my name is artem Hi my name is artem Hi my name is artem',
      time: new Date(),
      user: {
        id: '2',
        username: 'Artem Pliusnin',
        image: './chat-image.jpg',
      },
    },
    {
      id: '2',
      text: '.',
      time: new Date(),
      user: {
        id: '1',
        username: 'Artem Pliusnin',
        image: './chat-image.jpg',
      },
    },
  ];

  ngOnInit(): void {
    this.user = this.authService.user;
  }
}
