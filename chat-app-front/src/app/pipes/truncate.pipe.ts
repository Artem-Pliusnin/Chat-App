import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncate',
  standalone: true,
})
export class TruncatePipe implements PipeTransform {
  transform(value: string | null | undefined): string {
    if (!value) return '';
    const max = 40;
    const cut = 37;
    if (value.length <= max) return value;
    return value.slice(0, cut) + '...';
  }
}
