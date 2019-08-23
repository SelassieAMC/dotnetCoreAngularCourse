import { Router } from '@angular/router';
import { Vehicle } from './../models/Vehicle';
import { VehicleService } from './../services/vehicle.service';
import { Http } from '@angular/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.css']
})
export class VehiclesComponent implements OnInit {

  constructor(
    private http:Http, 
    private vehicleService: VehicleService,
    private router:Router
  ) { }

  vehicles : Vehicle[] = [];
  allVehicles : Vehicle[];
  makes : any;
  filter : any = {};
  pagination: number = 1;
  noNext: boolean = false;
  quantity: number = 6;
  ngOnInit() {
    //this.getVehicles(this.pagination,this.quantity);
    this.getAllVehicles();
    this.vehicleService.getMakes()
    .subscribe(m => {
      this.makes = m;
    })
  }
  private getVehicles(pagination,quantity){
    this.vehicleService.getVehicles(pagination,quantity).subscribe(vs=> {
      this.vehicles = this.allVehicles = vs;
      if(this.vehicles.length < 6){
        this.noNext = true;
      }else{
        this.noNext = false;
      }
    });
  }

  private getAllVehicles(){
    this.vehicleService.getAllVehicles().subscribe(vs=> {
      this.vehicles = this.allVehicles = vs;
    });
  }

  viewVehicle(id){
    this.router.navigate(['/vehicle/'+id]);
  }
  NextPage(){
    this.pagination++;
    this.getVehicles(this.pagination,this.quantity);
  }
  BackPage(){
    this.pagination--;
    this.getVehicles(this.pagination,this.quantity);
  }
  onFilterChange(){
    var vehicles = this.allVehicles;
    if(this.filter.makeId)
      vehicles = vehicles.filter(x => x.make.id == this.filter.makeId);
    
    this.vehicles = vehicles;
  }
  resetFilter(){
    this.filter = {};
    this.onFilterChange();
  }
}
