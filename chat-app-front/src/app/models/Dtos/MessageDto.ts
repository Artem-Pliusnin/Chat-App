import { UserDto } from "./UserDto";

export type MessageDto = {
  id: string;
  text: string;
  sender: UserDto;
  chatId: string;
  sendDate: string;
};
