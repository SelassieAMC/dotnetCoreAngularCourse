import { Router } from '@angular/router';
import { Vehicle } from './../models/Vehicle';
import { VehicleService } from './../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.css']
})
export class VehiclesComponent implements OnInit {

  constructor(
    private vehicleService: VehicleService,
    private router:Router
  ) { }
  queryResult : any = {};
  makes : any;
  query : any = {
    pageSize:5
  };
  pagination: number = 1;
  noNext: boolean = false;
  quantity: number = 6;
  columns: any =[
    {"title":"Id"},
    {"title":"Make","key":"make","isSortable":"true"},
    {"title":"Model","key":"model","isSortable":"true"},
    {"title":"Contact Name","key":"contactName","isSortable":"true"},
    {"title":""}
  ];
  ngOnInit() {
    this.getAllVehicles();
    this.vehicleService.getMakes()
    .subscribe(m => {
      this.makes = m;
    })
  }

  private getAllVehicles(){
    this.vehicleService.getAllVehicles(this.query).subscribe( result => {
      this.queryResult = result;
    });
  }
  onFilterChange(){
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
    console.log(this.columns);
    if(this.query.SortBy === columnName){
      this.query.IsSortAscending = !this.query.IsSortAscending;
    }else{
      this.query.SortBy = columnName;
      this.query.IsSortAscending = true;
    }
    this.getAllVehicles();
  }
  onPageChanged(page){
    this.query.page = page;
    this.getAllVehicles();
  }

}
