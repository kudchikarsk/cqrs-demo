import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-edit-customer',
    templateUrl: './edit-customer.component.html',
    styleUrls: ['./edit-customer.component.css']
})
export class EditCustomerComponent implements OnInit {
    customer: any = {};
    constructor(private customerService: CustomerService,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit() {
        this.customerService.get(this.route.snapshot.params.id)
            .subscribe((data) => {
                this.customer = data;
            }, (err) => {
                alert("Failed to load customer details");
                console.log(err);
            });
    }

    addAddress() {
        this.customer.addresses.push({});
        console.log(this.customer.addresses);
    }

    removeAddress(address) {
        let index = this.customer.addresses.indexOf(address);
        if (index > -1) {
            this.customer.addresses.splice(index,1);
        }        
    }

    save() {
        this.customerService.update(this.route.snapshot.params.id, this.customer)
            .subscribe(() => {
                this.router.navigateByUrl(`/view-customer/${this.customer.id}`);
            }, (err) => {
                alert("Failed to update customer details");
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
