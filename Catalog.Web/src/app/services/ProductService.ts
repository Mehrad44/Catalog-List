import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductSearchRequest } from '../models/product-search-request';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  productServiceUrl : string = 'https://localhost:5001/products'
  constructor(private http: HttpClient){

  }

  search(request : ProductSearchRequest): Observable<HttpResponse<Product[]>> {



    let queryParams = new HttpParams()
    .set('text',request.text)
        .set('text',request.sort)
            .set('text',request.pageSize)
                .set('text',request.pageIndex)

        let endpointUrl = `${this.productServiceUrl}/search`;


   return this.http.get<Product[]>(endpointUrl,{
    params : queryParams,
    observe : 'response'
   });


  
  }
}
