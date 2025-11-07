import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timePipe',
  standalone: true,
})
export class TimePipe implements PipeTransform {
  transform(value: Date): string {
    if (!value) return '';

    const date = new Date(value);
    const now = new Date();

    const isToday =
      date.getDate() === now.getDate() &&
      date.getMonth() === now.getMonth() &&
      date.getFullYear() === now.getFullYear();

    if (isToday) {
      return date.toLocaleTimeString([], {
        hour: '2-digit',
        minute: '2-digit',
      });
    }

    if (date.getFullYear() === now.getFullYear()) {
      return date.toLocaleDateString([], {
        day: '2-digit',
        month: '2-digit',
      });
    }

    return date.getFullYear().toString();
  }
}
