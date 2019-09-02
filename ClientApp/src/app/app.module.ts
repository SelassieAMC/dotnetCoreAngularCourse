import * as Sentry from '@sentry/browser';
import { AppErrorHandler } from './app.error-handlers';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
import { ToastrModule } from 'ng6-toastr-notifications';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PaginationComponent } from './shared/pagination/pagination.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { VehiclesComponent } from './vehicles/vehicles.component';
import { VehicleService } from './services/vehicle.service';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.component';

Sentry.init({
  dsn: 'https://5097dbc586e04ca6be34465a8dcab9e3@sentry.io/1532965'
});
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
    VehiclesComponent,
    PaginationComponent,
    VehicleDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    HttpModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    NgbModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
      { path: 'home', component: HomeComponent, pathMatch: 'full' },
      { path: 'vehicle/new', component: VehicleFormComponent},
      { path: 'vehicle/edit/:id', component: VehicleFormComponent},
      { path: 'vehicles', component: VehiclesComponent},
      { path: 'vehicle/details/:id', component: VehicleDetailsComponent},
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [
    {provide: ErrorHandler, useClass:AppErrorHandler},
    VehicleService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
