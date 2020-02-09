import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../services/customer.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    customers: any = [];

    constructor(private customerService: CustomerService) { }

    ngOnInit(): void {
        this.customerService.getAll().subscribe((data) => {
            this.customers = data;
        }, (err) => {
            alert(`Failed to load customers`);
        });
    }

    delete(customer) {
        this.customerService.delete(customer.id).subscribe((data) => {
            let index = this.customers.indexOf(customer);
            this.customers.splice(index, 1);
        }, (err) => {
            alert(`Failed to load customers`);
        });

    }
}
