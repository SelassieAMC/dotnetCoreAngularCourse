import * as _ from 'underscore';
import { Vehicle, SaveVehicle } from './../models/Vehicle';
import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ToastrManager  } from 'ng6-toastr-notifications';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin  } from 'rxjs';
@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  vehicle: SaveVehicle = {
    id:0,
    modelId:0,
    makeId:0,
    isRegistered:false,
    features : [],
    contact : {
      name:'',
      phone:'',
      email:''
    }
  };
  features = [];
  models: any[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService, 
    public toasterService : ToastrManager) {
      route.params.subscribe(r => {
        this.vehicle.id = +r['id'];
      });
    }

  ngOnInit() {   
    var sources = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures()];

    if(this.vehicle.id){
      sources.push(this.vehicleService.getVehicle(this.vehicle.id));
    }
    forkJoin(sources).subscribe(data => {
      this.makes = data[0];
      this.features = data[1];
      if(this.vehicle.id)
        this.setVehicle(data[2]);
    }, err => {
      if(err.status == 404)
        this.router.navigate(['']);
    });
  }

  private setVehicle(v : Vehicle){
    this.vehicle.id = v.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.isRegistered = v.isRegistered;   
    this.vehicle.contact = v.contact;
    this.vehicle.features = _.pluck(v.features,'id');
  }

  onMakeChange(){
    //console.log("VEHICLE", this.vehicle)
    var selectedMake = this.makes.find(item=>item.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models: [];
    delete this.vehicle.modelId;
  }
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
