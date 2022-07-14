import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'price'
})
export class PricePipe implements PipeTransform {

  transform(value?: number, ...args: unknown[]): string {
    if(!value){
      return '0.00 EUR'
    }

    return `${(value / 100).toFixed(2)} EUR`;
  }

}
