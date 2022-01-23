import { HttpClient } from '@angular/common/http';
import { ConstantPool } from '@angular/compiler';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  order: Order = { CustomerId: 1, RentalItems: [] }
  public equipment: Equipment[] = [];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Equipment[]>(baseUrl + 'Equipment').subscribe(result => {
      this.equipment = result;
    }, error => console.error(error));
  }

  public CreateOrder() {
  }

  public AddItemToOrder(ItemId: number, DaysOfRental: number) {

    console.log(event);
    let item: RentalItem = { EquipmentId: 0, DaysOfRental: 0 }
    item.DaysOfRental = DaysOfRental;
    item.EquipmentId = ItemId;
    this.order.RentalItems.push(item);
    console.log(this.order);
  }
}

interface Equipment {
  id: number,
  name: string
  type: string
}

interface Order {
  CustomerId: number,
  RentalItems: RentalItem[]
}

interface RentalItem {
  EquipmentId: number,
  DaysOfRental: number
}
