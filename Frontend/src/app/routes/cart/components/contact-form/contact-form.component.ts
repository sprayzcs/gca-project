import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Validators } from 'angular-reactive-validation';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.scss']
})
export class ContactFormComponent implements OnInit {

  formGroup!: FormGroup;
  currentYear: number;

  constructor(private readonly formBuilder: FormBuilder) { 
    this.currentYear = new Date().getFullYear();
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      email: new FormControl<string>('', [Validators.required('E-Mail muss angegeben werden'), Validators.email('E-Mail muss im Email Format sein')]),
      contact: this.formBuilder.group({
        street: new FormControl<string>('', [Validators.required('Straße muss angegeben werden')]),
        zipCode: new FormControl<number>(null!, [Validators.required('Postleitzahl muss angegeben werden'), Validators.min(10000, min => `Postleitzahl muss mindestens ${min} betragen`), Validators.max(99999, max => `Postleitzahl muss mindestens ${max} betragen`)]),
        city: new FormControl<string>('', [Validators.required('Stadt muss angegeben werden')]),
        country: new FormControl<string>('', [Validators.required('Land muss angegeben werden')]),
      }),
      payment: this.formBuilder.group({
        creditCardNumber: new FormControl<string>('', [Validators.required('Kreditkarten Nummer muss angegeben werden')]),
        month: new FormControl<number>(null!, [Validators.required('Monat muss angegeben werden')]),
        year: new FormControl<number>(null!, [Validators.required('Jahr muss angegeben werden')]),
        cvv: new FormControl<string>('', [Validators.required('CVV muss angegeben werden'), Validators.pattern('\\d{3}', 'CVV muss eine dreistellige Zahl sein')]),
      })
    });
  }

  get isValid(): boolean {
    return this.formGroup.valid;
  }

  get email(): string {
    return this.formGroup.get(['email'])?.value;
  }

  get street(): string {
    return this.formGroup.get(['contact', 'street'])?.value;
  }

  get zipCode(): string {
    return this.formGroup.get(['contact', 'zipCode'])?.value;
  }

  get city(): string {
    return this.formGroup.get(['contact', 'city'])?.value;
  }

  get country(): string {
    return this.formGroup.get(['contact', 'country'])?.value;
  }

  get creditCardNumber(): string {
    return this.formGroup.get(['payment', 'creditCardNumber'])?.value;
  }

  get creditCardExpiryDate(): Date {
    return new Date(
      this.formGroup.get(['payment', 'year'])?.value,
      this.formGroup.get(['payment', 'month'])?.value,
    )
  }

  get creditCardCvv(): string {
    return this.formGroup.get(['payment', 'cvv'])?.value;
  }

}
