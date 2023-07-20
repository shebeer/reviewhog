import { Component } from '@angular/core';
import { AppComponent } from '../app.component';

interface ProductType {
  title: string;
  avgRating: number;
  ratingScore: number;
  sentiment: string;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private appComponent: AppComponent) {}

  products: any = [];

  ngOnInit(): void {
    this.products = this.appComponent.products
  }

  select(id: number) {
    this.appComponent.select(id);
  }


}
