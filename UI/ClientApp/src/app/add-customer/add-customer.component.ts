import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-add-customer',
    templateUrl: './add-customer.component.html',
    styleUrls: ['./add-customer.component.css']
})
export class AddCustomerComponent implements OnInit {
    customer = {};

    constructor(private customerService: CustomerService,
        private router: Router) { }

    ngOnInit() {
    }


    create() {
        this.customerService.create(this.customer)
            .subscribe((data) => {
                this.router.navigateByUrl(`/view-customer/${data.Id}`);
            }, (err) => {
                alert(`Failed to create customer`);
                console.log(err);
            });
    }

}
