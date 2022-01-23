import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public customer: Customer = { name: "", loyaltyPoints: 0 }
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Customer>(baseUrl + 'Customer/1').subscribe(result => {
      this.customer = result;
    }, error => console.error(error));
  }
}

interface Customer {
  name: string;
  loyaltyPoints: number;
}
