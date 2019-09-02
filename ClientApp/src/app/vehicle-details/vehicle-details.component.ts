import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleService } from '../services/vehicle.service';
import { Vehicle } from '../models/Vehicle';
import * as _ from 'underscore';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.css']
})
export class VehicleDetailsComponent implements OnInit {
  vehicle: any;
  vehicleId:number;
  
  constructor(    
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService
    ) 
    { route.params.subscribe(r => {
      this.vehicleId = +r['id'];
      if(isNaN(this.vehicleId) || this.vehicleId<=0){
        router.navigate(['/vehicles']);
        return;
      }
    });
  }

  ngOnInit() {
    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(v=>{
        this.vehicle = v;
      }, err => {
      if(err.status == 404)
        this.router.navigate(['/vehicles']);
        return;
    });
  }

  goToEditForm(){
    this.router.navigate(['vehicle/edit/'+this.vehicle.id]);
  }

  deleteVehicle(){
    if(confirm("Are you sure?")){
      this.vehicleService.deleteVehicle(this.vehicle.id)
      .subscribe(res => {
        this.router.navigate(['/vehicles']);
      });
    }
  }

  goToVehicleList(){
    this.router.navigate(['vehicles']);
  }

}
