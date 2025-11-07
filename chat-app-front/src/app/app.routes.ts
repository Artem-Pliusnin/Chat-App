import { Routes } from '@angular/router';
import { AuthorizationPageComponent } from './pages/authorization-page/authorization-page.component';
import { ChatsPageComponent } from './pages/chats-page/chats-page.component';

export const routes: Routes = [
  {
    path: '',
    component: ChatsPageComponent,
  },
  {
    path: 'log-in',
    component: AuthorizationPageComponent,
  },
];
