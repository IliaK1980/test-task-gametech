import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of } from 'rxjs';
import { Currency } from './currency.model';

@Injectable({ providedIn: 'root' })
export class CurrenciesService {
  private currenciesUrl = 'api/currencies';

  constructor(private http: HttpClient) {}

  getCurrencies() {
    return this.http.get<Currency[]>(this.currenciesUrl).pipe(
      catchError((error) => {
        console.error(error);
        return of([] as Currency[]);
      })
    );
  }

  getExchangeRate(currencyId: number, date: string) {
    const url = `${this.currenciesUrl}/${currencyId}/rate?date=${date}`;
    return this.http.get(url).pipe(
      map((response: any) => {
        return response.rate;
      }),
      catchError((error) => {
        console.error(error);
        return of([] as Currency[]);
      })
    );
  }
}
