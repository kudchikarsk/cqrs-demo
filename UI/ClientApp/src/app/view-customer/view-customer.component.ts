import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-view-customer',
  templateUrl: './view-customer.component.html',
  styleUrls: ['./view-customer.component.css']
})
export class ViewCustomerComponent implements OnInit {
    customer = {};
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

}
