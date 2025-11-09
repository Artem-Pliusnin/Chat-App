import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'sentimentEmoji',
  standalone: true,
})
export class SentimentEmojiPipe implements PipeTransform {
  transform(value: number): string {
    switch (value) {
      case 0:
        return '😊';
      case 1:
        return '😐';
      case 2:
        return '😞';
      case 3:
        return '😕';
      default:
        return '';
    }
  }
}
