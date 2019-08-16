import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ToastrManager  } from 'ng6-toastr-notifications';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  vehicle: any= {
    features : [],
    Contact : {}
  };
  features = [];
  models: any[];

  constructor(private vehicleService: VehicleService, public toasterService : ToastrManager) { }

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
    var selectedMake = this.makes.find(item=>item.id == this.vehicle.MakeId);
    this.models = selectedMake ? selectedMake.models: [];
    delete this.vehicle.ModelId;
  }
  //My ontoggle features solution
  // addFeature(){
  //   const checkedOptions = this.features.filter(item => item.checked);
  //   //console.log(checkedOptions);
  //   //this.vehicle.features.push(checkedOptions.map(item => item.id));
  //   this.vehicle.features = checkedOptions.map(item => item.id);
  // }
  onFeatureToggle(featureId, $event){
    if($event.target.checked){
      this.vehicle.features.push(featureId);
    }else{
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit(){
    this.vehicleService.createVehicle(this.vehicle)
    .subscribe(
      x => console.log(x)
    );
  }

}
