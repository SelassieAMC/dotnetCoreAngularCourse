import { MakeService } from './../services/make.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  vehicle: any= {};
  models = {};

  constructor(private makeService: MakeService) { }

  ngOnInit() {
    this.makeService.getMakes()
    .subscribe(
      makes => {
        this.makes = makes;
        //console.log("MAKES", this.makes);
      });
  }

  onMakeChange(){
    //console.log("VEHICLE", this.vehicle)
    var selectedMake = this.makes.find(item=>item.id == this.vehicle.make);
    this.models = selectedMake ? selectedMake.models: [];
  }
}
