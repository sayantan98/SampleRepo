import { Component, Inject, OnInit } from '@angular/core';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
import { SharedService } from 'src/app/shared.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ShowDelComponent } from '../show-del/show-del.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css']
})
export class AddEditComponent implements OnInit {
  public title: string = '';
  public dep: any;
  constructor(
    public modalRef: MdbModalRef<AddEditComponent>,
    private service: SharedService,
    private toastr: ToastrService,
    private router: Router,
    private loader: NgxSpinnerService
    ) {  }
  ngOnInit(): void {
    
  }
  Save(heroForm: NgForm){
    this.loader.show();
    if(heroForm.valid){
      this.service.InsertDepartment(this.dep).subscribe(res =>{
        this.toastr.success(res, 'Insert Completed!');
        this.modalRef.close();
        this.loader.hide();
      },
      error => {
        this.toastr.error(error.statusText, "Error!");
        this.loader.hide();
      });
    }
    else{
      this.toastr.warning("Please enter all the data","Warning");
      this.loader.hide();
    }
  }
  Update(heroForm: NgForm){
    this.loader.show();
    if(heroForm.valid){
      this.service.UpdateDepartment(this.dep).subscribe(res =>{
        this.toastr.success(res, 'Update Completed!');
        this.modalRef.close();
        this.loader.hide();
      },
      error => {
        this.toastr.error(error.statusText, "Error!");
        this.loader.hide();
      })
    }
    else{
      this.toastr.warning("Please enter all the data","Warning");
      this.loader.hide();
    }
  }
}
