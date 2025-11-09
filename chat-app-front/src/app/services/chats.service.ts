import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { ChatDto } from '../models/Dtos/ChatDto';
import { NewChatDto } from '../models/Dtos/NewChatDto';
import { AuthorizationService } from './authorization.service';

@Injectable({
  providedIn: 'root',
})
export class ChatsService {
  private apiUrl = `${environment.apiUrl}/chats`;

  private authService = inject(AuthorizationService);
  constructor(private http: HttpClient) {}

  getCurrentUserChats(): Observable<ChatDto[]> {
    return this.http.get<ChatDto[]>(`${this.apiUrl}/current`);
  }

  createChat(dto: NewChatDto): Observable<ChatDto> {
    dto.memberIds.push(this.authService.user.id);
    return this.http.post<ChatDto>(this.apiUrl, dto);
  }
}
