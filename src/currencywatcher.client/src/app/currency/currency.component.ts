import { Component, OnDestroy, OnInit } from '@angular/core';
import { CurrenciesService } from './currencies.service';
import { Currency } from './currency.model';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-currency',
  templateUrl: './currency.component.html',
})
export class CurrencyComponent implements OnInit, OnDestroy {
  currencyForm!: FormGroup;
  currencies: Currency[] = [];
  rate: number;
  currenciesSubscription: Subscription;
  rateSubscription: Subscription;

  constructor(private currenciesService: CurrenciesService) {}

  ngOnInit(): void {
    this.currenciesSubscription = this.currenciesService
      .getCurrencies()
      .subscribe((currencies: Currency[]) => {
        this.currencies = currencies;
      });

    this.initForm();
  }

  ngOnDestroy(): void {
    this.currenciesSubscription.unsubscribe();
    if (this.rateSubscription)
    {
      this.rateSubscription.unsubscribe();
    }
  }

  onSubmit() {
    this.rateSubscription = this.currenciesService
      .getExchangeRate(
        +this.currencyForm.value.currency,
        this.currencyForm.value.date
      )
      .subscribe((rate: number) => {
        this.rate = rate;
      });
  }

  private initForm() {
    let currencyRateDate = new Date();
    this.currencyForm = new FormGroup({
      currency: new FormControl(null, Validators.required),
      date: new FormControl(currencyRateDate, Validators.required),
    });
  }
}
