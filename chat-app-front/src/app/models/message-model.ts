import { UserInfoModel } from './user-info-model';

export type MessageModel = {
  id: string;
  text: string;
  time: Date;
  user: UserInfoModel;
};
