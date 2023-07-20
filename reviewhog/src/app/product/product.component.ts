import { Component } from '@angular/core';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  constructor(private appComponent: AppComponent) {}
  product: any = {}
  ngOnInit(): void {
    this.product = this.appComponent.selectedItem
  }

  goTo(url: string) {
    this.appComponent.goTo(url);
  }

}
