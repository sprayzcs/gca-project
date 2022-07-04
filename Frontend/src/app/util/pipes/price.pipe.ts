import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'price'
})
export class PricePipe implements PipeTransform {

  transform(value: number, ...args: unknown[]): string {
    return `${(value / 100).toFixed(2)} EUR`;
  }

}