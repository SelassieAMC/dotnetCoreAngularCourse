import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(private http: Http) { }

  upload(vehicleId, photo){
    var formData = new FormData();
    formData.append('file', photo);
    return this.http.post(`/api/vehicle/${vehicleId}/photos`,formData)
    .pipe(map(res=> res.json()));
  }

  getPhotos(vehicleId){
    return this.http.get(`/api/vehicle/${vehicleId}/photos`)
    .pipe(map(res=> res.json()));
  }
}