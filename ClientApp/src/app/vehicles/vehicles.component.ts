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
  makes : any;
  query : any = {};
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
      this.vehicles = vs;
      if(this.vehicles.length < 6){
        this.noNext = true;
      }else{
        this.noNext = false;
      }
    });
  }

  private getAllVehicles(){
    this.vehicleService.getAllVehicles(this.query).subscribe(vs=> {
      this.vehicles = vs;
    });
  }
  onFilterChange(){
    //this.filter.modelId =2;
    this.getAllVehicles();
  }
  resetFilter(){
    this.query = {};
    this.onFilterChange();
  }
  viewVehicle(id){
    this.router.navigate(['/vehicle/'+id]);
  }
  orderBy(columnName){
    if(this.query.SortBy === columnName){
      this.query.IsSortAscending = false;
    }else{
      this.query.SortBy = columnName;
      this.query.IsSortAscending = true;
    }
    this.getAllVehicles();
  }

  NextPage(){
    this.pagination++;
    this.getVehicles(this.pagination,this.quantity);
  }
  BackPage(){
    this.pagination--;
    this.getVehicles(this.pagination,this.quantity);
  }
}
