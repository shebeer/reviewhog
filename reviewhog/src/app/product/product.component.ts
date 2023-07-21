import { Component } from '@angular/core';
import { AppComponent } from '../app.component';


interface PlatformInfo {
  link: string;
  avgRating: number;
  totalRating: number;
}

interface Product {
  title: string;
  avgRating: number;
  ratingScore: number;
  sentiment: string;
  description: string;
  image: string;
  id: string;
  platforms: {
    amazon: PlatformInfo;
    walmart: PlatformInfo;
    target: PlatformInfo;
    samsclub: PlatformInfo;
  };
  positiveKeywords: string[];
  negativeKeywords: string[];
}

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  constructor(private appComponent: AppComponent) {}
  product: Product = {
    title: "",
    avgRating: 0,
    ratingScore: 0,
    sentiment: "",
    description: "",
    image: "",
    id: "",
    platforms: {
      amazon: {
  link: "",
  avgRating: 0,
  totalRating: 0,
},
      walmart: {
  link: "",
  avgRating: 0,
  totalRating: 0,
},
      target: {
  link: "",
  avgRating: 0,
  totalRating: 0,
},
      samsclub: {
  link: "",
  avgRating: 0,
  totalRating: 0,
},
    },
    positiveKeywords: [],
    negativeKeywords: [],
  }
  ngOnInit(): void {
    this.product = this.appComponent.selectedItem
  }

  goTo(url: string) {
    this.appComponent.goTo(url);
  }

}
