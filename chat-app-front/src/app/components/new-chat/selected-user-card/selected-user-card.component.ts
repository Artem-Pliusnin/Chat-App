import { Component, Input } from '@angular/core';
import { UserInfoModel } from '../../../models/user-info-model';
import { Emitters } from '../../../emitters/emitters';

@Component({
  selector: 'app-selected-user-card',
  standalone: true,
  imports: [],
  templateUrl: './selected-user-card.component.html',
  styleUrl: './selected-user-card.component.css',
})
export class SelectedUserCardComponent {
  @Input({ required: true }) user!: UserInfoModel;

  OnDelete() {
    Emitters.delteUserEmitter.emit(this.user);
  }
}
