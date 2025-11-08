import { EventEmitter } from '@angular/core';
import { UserInfoModel } from '../models/user-info-model';
import { ChatCardModel } from '../models/chat-card-model';
import { MessageModel } from '../models/message-model';

export class Emitters {
  static addingNewChat = new EventEmitter<boolean>();
  static delteUserEmitter = new EventEmitter<UserInfoModel>();
  static addUserEmitter = new EventEmitter<UserInfoModel>();
  static addChatEmitter = new EventEmitter<ChatCardModel>();
  static newMessageEmitter = new EventEmitter<MessageModel>();
}
