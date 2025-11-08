import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NewMessageDto } from '../models/Dtos/NewMessageDto';
import { MessageDto } from '../models/Dtos/MessageDto';

@Injectable({
  providedIn: 'root',
})
export class MessagesService {
  private apiUrl = `${environment.apiUrl}/messages`;

  constructor(private http: HttpClient) {}

  getMessagesByChatId(chatId: string): Observable<MessageDto[]> {
    return this.http.get<MessageDto[]>(`${this.apiUrl}/${chatId}`);
  }

  createMessage(dto: NewMessageDto): Observable<MessageDto> {
    return this.http.post<MessageDto>(this.apiUrl, dto);
  }
}
