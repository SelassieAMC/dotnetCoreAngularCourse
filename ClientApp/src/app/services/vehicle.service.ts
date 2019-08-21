import { Vehicle, SaveVehicle } from './../models/Vehicle';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from "rxjs/operators"; 

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private http: Http) { }

  getMakes(){
    return this.http.get('/api/vehicle/getMakes')
      .pipe(map(res => res.json()));
  }
  getFeatures(){
    return this.http.get('/api/vehicle/getFeatures')
      .pipe(map(res => res.json()));
  }
  createVehicle(vehicle){
    return this.http.post('/api/vehicle/addVehicle',vehicle)
      .pipe(map(res=> res.json()));
  }

  getVehicle(id){
    return this.http.get('/api/vehicle/getVehicle/'+id)
    .pipe(map(res=>res.json()));
  }

  updateVehicle(vehicle : SaveVehicle){
    return this.http.post('/api/vehicle/updateVehicle/'+vehicle.id, vehicle)
    .pipe(map(res=>res.json()));
  }

  deleteVehicle(id){
    return this.http.get('/api/vehicle/deleteVehicle/'+id)
    .pipe(map(res=>res.json()));
  }

  getVehicles(pagination, quantity){
    return this.http.get('/api/vehicle/getVehicles/'+pagination+'/'+quantity)
    .pipe(map(res=>res.json()));
  }
}
