import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NewMessageDto } from '../models/Dtos/NewMessageDto';
import { MessageDto } from '../models/Dtos/MessageDto';
import { MessageModel } from '../models/message-model';
import * as signalR from '@microsoft/signalr';
import { Emitters } from '../emitters/emitters';
import { ChatDto } from '../models/Dtos/ChatDto';

@Injectable({
  providedIn: 'root',
})
export class MessagesService {
  private apiUrl = `${environment.apiUrl}/messages`;
  private hubConnection!: signalR.HubConnection;

  constructor(private http: HttpClient) {}

  getMessagesByChatId(chatId: string): Observable<MessageDto[]> {
    return this.http.get<MessageDto[]>(`${this.apiUrl}/${chatId}`);
  }

  createMessage(dto: NewMessageDto): Observable<MessageDto> {
    return this.http.post<MessageDto>(this.apiUrl, dto);
  }

  markAsRead(messageId: string) {
    this.http.post(`${this.apiUrl}/${messageId}/mark-as-read`, {}).subscribe();
  }

  async connect(): Promise<void> {
    if (
      this.hubConnection &&
      this.hubConnection.state === signalR.HubConnectionState.Connected
    ) {
      console.log('Already connected to SignalR');
      return;
    }

    const token = localStorage.getItem('jwt');

    if (!token) {
      console.error('No JWT token found, cannot connect to SignalR');
      return;
    }

    try {
      const negotiateData = await this.http
        .post<any>(`${environment.hubUrl}/chathub/negotiate`, {})
        .toPromise();

      if (!negotiateData.url || !negotiateData.accessToken) {
        throw new Error(
          'Invalid negotiate response: missing url or accessToken'
        );
      }

      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(negotiateData.url, {
          accessTokenFactory: () => negotiateData.accessToken,
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets,
        })
        .withAutomaticReconnect({
          nextRetryDelayInMilliseconds: (retryContext) => {
            if (retryContext.previousRetryCount === 0) return 0;
            if (retryContext.previousRetryCount === 1) return 2000;
            if (retryContext.previousRetryCount === 2) return 10000;
            return 30000;
          },
        })
        .configureLogging(signalR.LogLevel.Information)
        .build();

      await this.hubConnection.start();

      this.hubConnection.on('ReceiveMessage', (message: MessageDto) => {
        const newMessage: MessageModel = {
          id: message.id,
          text: message.text,
          time: new Date(message.sendDate),
          user: {
            id: message.sender.id,
            username: message.sender.userName,
            image: './chat-image.jpg',
          },
          chatId: message.chatId,
          sentiment: message.sentiment,
        };

        Emitters.newMessageEmitter.emit(newMessage);
      });

      this.hubConnection.on('NewChatCreated', (chat: ChatDto) => {
        Emitters.addChatEmitter.emit(chat);
      });
    } catch (err: any) {
      console.error('Error connecting to SignalR hub:', err);
      console.error('Error details:', {
        message: err.message,
        stack: err.stack,
        statusCode: err.statusCode,
      });
      throw err;
    }
  }

  async disconnect() {
    if (this.hubConnection) {
      await this.hubConnection.stop();
      this.hubConnection = null!;
      console.log('SignalR disconnected');
    }
  }
}
