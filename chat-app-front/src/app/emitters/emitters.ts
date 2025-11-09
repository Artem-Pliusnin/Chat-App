import { EventEmitter } from '@angular/core';
import { UserInfoModel } from '../models/user-info-model';
import { ChatCardModel } from '../models/chat-card-model';
import { MessageModel } from '../models/message-model';
import { ChatDto } from '../models/Dtos/ChatDto';

export class Emitters {
  static addingNewChat = new EventEmitter<boolean>();
  static delteUserEmitter = new EventEmitter<UserInfoModel>();
  static addUserEmitter = new EventEmitter<UserInfoModel>();
  static addChatEmitter = new EventEmitter<ChatDto>();
  static newMessageEmitter = new EventEmitter<MessageModel>();
}
