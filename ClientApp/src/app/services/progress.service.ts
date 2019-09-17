import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { BrowserXhr } from '@angular/http';

@Injectable()
export class ProgressService {
    uploadProgress: Subject<any> = new Subject();
    downloadProgess: Subject<any> = new Subject();
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
            this.service.uploadProgress.next(this.createProgress(event));
        };
        return xhr;
    }
    
    private createProgress(event){
        return {
            total: event.total,
            percentaje: Math.round(event.loaded /event.total * 100)
        }
    }
}