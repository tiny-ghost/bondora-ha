import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { OrderDto } from '../Models/OrderDto';
import { OrderViewModel } from '../Models/OrderViewModel';
import { RentalItem } from '../Models/RentalItem';
import { RentalService } from '../services/rental.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {


  equipment: Equipment[] = [];
  customerOrders: OrderViewModel[] = [];
  rentalItemData: RentalItem[] = [];
  cartDataSource = new TableDataSource<RentalItem>(this.rentalItemData);
  orderDataSource = new TableDataSource<OrderViewModel>(this.customerOrders);
  order: OrderDto = { CustomerId: 1, RentalItems: [] } //Customer id is always 1 as there is only one customer
  selectedEquip: Equipment = { id: 0, name: "", type: "" };
  cartDisplayedColumns: string[] = ['name', 'type', 'duration', 'action'];
  orderDisplayedColumns: string[] = ['id', 'items', 'action']
  daysOfRent = 1;

  constructor(private _rentalService: RentalService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Equipment[]>(baseUrl + 'Equipment').subscribe(result => {
      console.log(result);
      this.equipment = result;
      this.selectedEquip = this.equipment[0];
    }, error => console.error(error));
  }
  ngOnInit() {

    this.getOrdersByCustomerId(1);
  }

  getOrdersByCustomerId(customerId: number): void {
    this._rentalService.getCustomerOrder(customerId).subscribe(res => {
      this.customerOrders = res;
      this.orderDataSource.setData(this.customerOrders);
    })
  }

  getInvoice(orderId: number) {
    window.open(`/Invoice/${orderId}`);
  }

  PlaceOrder() {

    this._rentalService.PlaceOrder(this.order).subscribe(() => {
      this.getOrdersByCustomerId(1);
    });


    this.order = { CustomerId: 1, RentalItems: [] };
  }

  AddItemToOrder() {

    let item: RentalItem = this.createRentalItem();

    this.order.RentalItems.push(item);

    this.updateCartTable();

    this.resetForm();

  }

  removeItemFromOrder(item: RentalItem) {

    let index = this.order.RentalItems.indexOf(item);
    if (index !== -1) {
      this.order.RentalItems.splice(index, 1);
      this.cartDataSource.setData(this.order.RentalItems);
    }

  }

  private createRentalItem(): RentalItem {
    let item: RentalItem = { Name: "", Type: "", EquipmentId: 0, DaysOfRental: 0 }
    item.Name = this.selectedEquip.name;
    item.Type = this.selectedEquip.type;
    item.DaysOfRental = this.daysOfRent;
    item.EquipmentId = this.selectedEquip.id;
    return item;
  }

  private updateCartTable(): void {

    this.cartDataSource.setData(this.order.RentalItems);
  }

  private resetForm(): void {
    this.daysOfRent = 1;

  }


}

interface Equipment {
  id: number,
  name: string
  type: string
}

class TableDataSource<T> extends DataSource<T>{

  private _dataStream = new ReplaySubject<T[]>();

  constructor(initialData: T[]) {
    super();
    this.setData(initialData);
  }
  disconnect(collectionViewer: CollectionViewer): void {

  }

  connect(collectionViewer: CollectionViewer): Observable<readonly T[]> {
    return this._dataStream;
  }

  setData(data: T[]) {
    this._dataStream.next(data);
  }
}




