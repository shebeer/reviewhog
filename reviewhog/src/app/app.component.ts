import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

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
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  products: Product[] = [
    {
      title: 'Pr1',
      avgRating: 4.5,
      ratingScore: 60,
      sentiment: 'Positive',
      description: "description2",
      image: "https://m.media-amazon.com/images/I/81fRAoUL-fL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
      id: "1",
      platforms: {
        amazon: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        },
        walmart: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        },
        target: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        },
        samsclub: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        }
      },
      positiveKeywords: ['key1', 'key2', 'key3', 'key4'],
      negativeKeywords: ['key1', 'key2', 'key3', 'key4'],
    },
    {
      title: 'Pr2',
      avgRating: 4.5,
      ratingScore: 60,
      sentiment: 'Negative',
      description: "description 4",
      image: "https://m.media-amazon.com/images/I/71zYDBVeMYL._AC_SL1500_.jpg",
      id: "2",
      platforms: {
        amazon: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        },
        walmart: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        },
        target: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        },
        samsclub: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        }
      },
      positiveKeywords: ['key1', 'key2', 'key3', 'key4'],
      negativeKeywords: ['key1', 'key2', 'key3', 'key4'],
    },
    {
      title: 'Pr3',
      avgRating: 4.5,
      ratingScore: 60,
      sentiment: 'Neutral',
      description: "description 7",
      image: "https://m.media-amazon.com/images/I/51hKD47+oLL._AC_SL1500_.jpg",
      id: "3",
      platforms: {
        amazon: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        },
        walmart: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        },
        target: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        },
        samsclub: {
          link: "https://www.amazon.com/Anolon-Nonstick-Cooking-Utensils-Kitchen/dp/B084CYV4L1",
          avgRating: 3.5,
          totalRating: 100
        }
      },
      positiveKeywords: ['key1', 'key2', 'key3', 'key4'],
      negativeKeywords: ['key1', 'key2', 'key3', 'key4'],
    },
  ];

  selectedItem: any = {};

  constructor(private router: Router, private elRef: ElementRef) {}

  title = 'Review Hog';

  goTo(url: string) {
    this.router.navigateByUrl(url);
    this.elRef.nativeElement
      .querySelector('#main_nav')
      .classList.remove('show');
    this.elRef.nativeElement
      .querySelector('.animated-icon1')
      .classList.remove('open');
  }

  select(id: string) {
    let item = {}
    this.products.forEach(function (value) {
      if (value.id == id){
        item = value
      }
    });
    this.selectedItem = item
    this.goTo("/product")
  }
}
