import { Component, Input } from '@angular/core';
import { MessageModel } from '../../../models/message-model';
import { TimePipe } from '../../../pipes/time.pipe';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [TimePipe],
  templateUrl: './message.component.html',
  styleUrl: './message.component.css',
})
export class MessageComponent {
  @Input({ required: true }) message!: MessageModel;
  @Input({ required: true }) isMine!: boolean;
}
