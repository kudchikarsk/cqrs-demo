import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

    constructor(private http: HttpClient) { }

    getAll(): Observable<any> {
        return this.http.get(`${environment.apiUrl}api/customers`);
    }

    get(id:number): Observable<any> {
        return this.http.get(`${environment.apiUrl}api/customers/${id}`);
    }

    create(customer): Observable<any> {
        return this.http.post(`${environment.apiUrl}api/customers`, customer);
    }

    update(id, customer): Observable<any> {
        return this.http.put(`${environment.apiUrl}api/customers/${id}`, customer);
    }

    delete(id): Observable<any> {
        return this.http.delete(`${environment.apiUrl}api/customers/${id}`);
    }

}
