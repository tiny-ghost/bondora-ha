import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OrderDto } from '../Models/OrderDto';
import { OrderViewModel } from '../Models/OrderViewModel';

@Injectable()
export class RentalService {

  private placeOrderApi: string;
  private getCustomerOrdersApi: string;
  private getInvoiceApi: string;

  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.placeOrderApi = baseUrl + "Order";
    this.getCustomerOrdersApi = baseUrl + "Order/customer";
    this.getInvoiceApi = baseUrl + "Invoice";
  }

  public PlaceOrder(data: OrderDto): Observable<any> {
    return this._http.post<any>(this.placeOrderApi, data)
  }

  public getCustomerOrder(customerId: number): Observable<OrderViewModel[]> {
    return this._http.get<OrderViewModel[]>(this.getCustomerOrdersApi + `/${customerId}`);
  }

  public getOrderInvoice(orderId: number): Observable<any> {
    return this._http.get(this.getInvoiceApi + `/${orderId}`, { responseType: 'text' });
  }
}
