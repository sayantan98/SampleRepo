import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { MdbModalService, MdbModalRef  } from 'mdb-angular-ui-kit/modal';
import { AddEditComponent } from '../add-edit/add-edit.component';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-show-del',
  templateUrl: './show-del.component.html',
  styleUrls: ['./show-del.component.css']
})
export class ShowDelComponent implements OnInit {
  modalRef: MdbModalRef<AddEditComponent> | null = null;
  constructor(private apiservice: SharedService, private modalService: MdbModalService, private toastr: ToastrService, private loader: NgxSpinnerService) { 
  }
  
  deplist:any = [];
  dep:any;
  Modaltitle:string="";
  ngOnInit(): void {
    this.refreshDepList();
  }
  refreshDepList(){
    this.loader.show();
    this.apiservice.GetDepartmentList().subscribe(
      dep =>{
        this.deplist = dep;
        this.loader.hide();
      },
      error => {
        this.toastr.error(error.statusText, "Error!");
        this.loader.hide();

      }
    );
  }
  addClick(){
    this.dep = {
      departmentId:0,
      departmentName:""
    };
    this.Modaltitle = "Add Department";
    this.modalRef = this.modalService.open(AddEditComponent, {
      data: {
        title: this.Modaltitle,
        dep: this.dep
      }
    });
    this.modalRef.onClose.subscribe(() => {
      this.refreshDepList();
    });
  }
  editClick(item: any){
    this.dep = item;
    this.Modaltitle = "Update Department";
    this.modalRef = this.modalService.open(AddEditComponent, {
      data: {
        title: this.Modaltitle,
        dep: this.dep
      }
    });
    this.modalRef.onClose.subscribe(() =>{
      this.refreshDepList();
    })
  }
  deleteClick(item: any){
    this.loader.show();
    this.apiservice.DeleteDepartment(item).subscribe(res => {
      this.toastr.success(res, "Deleted Successfully!");
      this.refreshDepList();
      this.loader.hide();
    },
    error => {
      this.toastr.error(error.statusText, "Error!");
      this.loader.hide();
    }
    );
  }
}
