import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DepartmentComponent } from './department/department.component';
import { ShowDelComponent } from './department/show-del/show-del.component';
import { AddEditComponent } from './department/add-edit/add-edit.component';
import { SharedService } from './shared.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MdbModalService } from 'mdb-angular-ui-kit/modal';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { EmployeeComponent } from './employee/employee.component';
import { ShowDelEmployeeComponent } from './employee/show-del/show-del.component';
import { AddEditEmployeeComponent } from './employee/add-edit/add-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    DepartmentComponent,
    ShowDelComponent,
    AddEditComponent,
    EmployeeComponent,
    AddEditEmployeeComponent,
    ShowDelEmployeeComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 2000,
      positionClass: 'toast-top-right',
      closeButton: true,
      progressBar: true
    }),
    NgxSpinnerModule
  ],
  providers: [SharedService, MdbModalService, ToastrService],
  bootstrap: [AppComponent]
})
export class AppModule { }
