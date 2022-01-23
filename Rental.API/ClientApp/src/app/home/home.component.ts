import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  public equipment: Equipment[] = [];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Equipment[]>(baseUrl + 'Equipment').subscribe(result => {
      this.equipment = result;
    }, error => console.error(error));
  }
}

interface Equipment {
  name: string
  type: string
}
