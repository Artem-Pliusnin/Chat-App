import { EventEmitter } from '@angular/core';
import { UserInfoModel } from '../models/user-info-model';

export class Emitters {
  static addingNewChat = new EventEmitter<boolean>();
  static delteUserEmitter = new EventEmitter<UserInfoModel>();
  static addUserEmitter = new EventEmitter<UserInfoModel>();
}
