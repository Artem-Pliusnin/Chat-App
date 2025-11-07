import { Injectable } from '@angular/core';
import { UserInfoModel } from '../models/user-info-model';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationService {
  user!: UserInfoModel;

  logIn(): boolean {
    this.user = {
      id: '2',
      username: 'Artem Pliusnin',
      image: './chat-image.jpg',
    };
    return true;
  }
  constructor() {}
}
