import * as Sentry from '@sentry/browser';
import { ErrorHandler, Injectable, Injector, isDevMode } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';
@Injectable()
export class AppErrorHandler implements ErrorHandler{
    private _toastService?:ToastrManager; //solving Cannot instantiate cyclic dependency!
    /**
     *
     */
    constructor(private _injector:Injector) {        
    }
    handleError(error: any): void {
        //solving cyclic dependency
        if (!this._toastService) {
            this._toastService=this._injector.get(ToastrManager);
        }
        if(!isDevMode()){
            Sentry.captureException(error.originalError || error);
        }
        //throw error;
        //console.log("Method not implemented.");
        this._toastService.errorToastr(
            'An expected error hapenned',
            'Error',
            {
              toastTimeout:5000,
              animate: 'slideFromTop',
              showCloseButton: true
          });
    }
}