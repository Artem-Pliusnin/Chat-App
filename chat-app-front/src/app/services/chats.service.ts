import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { ChatDto } from '../models/Dtos/ChatDto';
import { NewChatDto } from '../models/Dtos/NewChatDto';

@Injectable({
  providedIn: 'root',
})
export class ChatsService {
  private apiUrl = `${environment.apiUrl}/chats`;

  constructor(private http: HttpClient) {}

  getCurrentUserChats(): Observable<ChatDto[]> {
    return this.http.get<ChatDto[]>(`${this.apiUrl}/current`);
  }

  createChat(dto: NewChatDto): Observable<ChatDto> {
    return this.http.post<ChatDto>(this.apiUrl, dto);
  }
}
