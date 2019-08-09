import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  vehicle: any= {};
  features = [];
  models: any[];

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes()
    .subscribe(
      makes => {
        this.makes = makes;
        //console.log("MAKES", this.makes);
      });
    this.vehicleService.getFeatures()
      .subscribe(features => this.features = features);
  }

  onMakeChange(){
    //console.log("VEHICLE", this.vehicle)
    var selectedMake = this.makes.find(item=>item.id == this.vehicle.make);
    this.models = selectedMake ? selectedMake.models: [];
  }

  addFeature(){
    this.vehicle.features = [];
    const checkedOptions = this.features.filter(item => item.checked);
    //console.log(checkedOptions);
    this.vehicle.features.push(checkedOptions.map(item => item.id));
  }

  saveVehicleData(){
    console.log(this.vehicle);
  }

}
