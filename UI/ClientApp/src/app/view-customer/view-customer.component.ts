import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-view-customer',
  templateUrl: './view-customer.component.html',
  styleUrls: ['./view-customer.component.css']
})
export class ViewCustomerComponent implements OnInit {
    customer = { addresses:[] };
    address = { };
    constructor(private customerService: CustomerService,
        private route: ActivatedRoute) { }

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
        this.customerService.addAddress(this.route.snapshot.params.id, this.address)
            .subscribe((data) => {
                this.customer.addresses.push(data);
                this.address = {};
            }, (err) => {
                alert("Failed to load customer details");
                console.log(err);
            });
    }

    removeAddress(address) {
        this.customerService.removeAddress(this.route.snapshot.params.id, address.id)
            .subscribe(() => {
                var index = this.customer.addresses.indexOf(address);
                if (index > -1) {
                    this.customer.addresses.splice(index, 1);
                    
                }
            }, (err) => {
                alert("Failed to load customer details");
                console.log(err);
            });
    }

    markPrimary(address) {

        this.customerService.markPrimaryAddress(this.route.snapshot.params.id, address.id)
            .subscribe(() => {
                address.isPrimary = true;
                this.customer.addresses
                    .filter(a => a != address)
                    .map(a => {
                        a.isPrimary = false;
                        return null;
                    });
            }, (err) => {
                alert("Failed to load customer details");
                console.log(err);
            });

        
    }
}
