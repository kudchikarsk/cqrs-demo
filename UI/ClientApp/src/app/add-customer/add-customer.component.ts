import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-add-customer',
    templateUrl: './add-customer.component.html',
    styleUrls: ['./add-customer.component.css']
})
export class AddCustomerComponent implements OnInit {
    customer = {
        addresses: [{ isPrimary:false}]
    };

    constructor(private customerService: CustomerService,
        private router: Router) { }

    ngOnInit() {
    }

    addAddress() {
        this.customer.addresses.push({ isPrimary: false});
    }

    removeAddress(address) {
        let index = this.customer.addresses.indexOf(address);
        if (index > -1) {
            this.customer.addresses.splice(index, 1);
        }
    }

    create() {
        this.customerService.create(this.customer)
            .subscribe((data) => {
                this.router.navigateByUrl(`/`);
            }, (err) => {
                alert(`Failed to create customer`);
                console.log(err);
            });
    }

    markedPrimary(address) {
        this.customer.addresses
            .filter(a => a != address)
            .map(a => {
                a.isPrimary = false;
                return null;
            });
    }

}
