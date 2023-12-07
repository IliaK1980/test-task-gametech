export class Currency {
  public id: number;
  public currencyCode: string;

  constructor(id: number, currencyCode: string) {
    this.id = id;
    this.currencyCode = currencyCode;
  }
}
