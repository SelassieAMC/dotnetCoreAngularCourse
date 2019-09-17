import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { BrowserXhr } from '@angular/http';

@Injectable()
export class ProgressService {
    private uploadProgress: Subject<any>;
    downloadProgess: Subject<any> = new Subject();

    startTracking(){
        this.uploadProgress = new Subject();
        return this.uploadProgress;
    }

    notify(progress){
        if(this.uploadProgress)
            this.uploadProgress.next(progress);
    }

    stopTracking(){
        if(this.uploadProgress)
            this.uploadProgress.complete();
    }
}

@Injectable()
export class BrowserXhrWithProgress extends BrowserXhr{
    constructor( private service: ProgressService){
        super();
    }

    build(): XMLHttpRequest{
        var xhr : XMLHttpRequest = super.build();
        
        xhr.onprogress = (event) =>{
            this.service.downloadProgess.next(this.createProgress(event));
        };
        
        xhr.upload.onprogress = (event) =>{
            this.service.notify(this.createProgress(event));
        };
        xhr.upload.onloadend = () => {
            this.service.stopTracking();
        }
        return xhr;
    }
    
    private createProgress(event){
        return {
            total: event.total,
            percentaje: Math.round(event.loaded /event.total * 100)
        }
    }
}