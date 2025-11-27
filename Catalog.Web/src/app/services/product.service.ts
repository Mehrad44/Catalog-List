import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { ProductSearch } from '../models/product-search.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  productServiceUrl: string = 'https://localhost:5001/products';

  constructor(private http: HttpClient) { }

  searchProducts(searchData: ProductSearch): Observable<HttpResponse<Product[]>> {
    let endPointUrl: string = `${this.productServiceUrl}/search`;
    let queryParams = new HttpParams()
      .set('text', searchData.searchText)
      .set('pageSize', searchData.pageSize)
      .set('pageIndex', searchData.pageIndex)
      .set('sort', searchData.sort);

    return this.http.get<Product[]>(endPointUrl, {
      params: queryParams,
      observe: 'response'
    });
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.productServiceUrl, product);
  }

  getProductById(productId: string): Observable<Product> {
    let endPointUrl: string = `${this.productServiceUrl}/${productId}`;
    return this.http.get<Product>(endPointUrl);
  }

  updateProduct(productId: string, product: Product): Observable<never> {
    let endPointUrl: string = `${this.productServiceUrl}/${productId}`;
    return this.http.put<never>(endPointUrl, product);
  }

  deleteProduct(productId: string): Observable<never> {
    let endPointUrl: string = `${this.productServiceUrl}/${productId}`;
    return this.http.delete<never>(endPointUrl);
  }
}
