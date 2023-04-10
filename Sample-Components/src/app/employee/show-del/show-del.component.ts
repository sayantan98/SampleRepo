import { Component } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AddEditEmployeeComponent } from '../add-edit/add-edit.component';
import { formatDate } from '@angular/common';
import { DatePipe } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-show-del-employee',
  templateUrl: './show-del.component.html',
  styleUrls: ['./show-del.component.css']
})
export class ShowDelEmployeeComponent {
  constructor(private apiService: SharedService, private modal: MdbModalService, private toast: ToastrService, private loader: NgxSpinnerService,private _sanitizer: DomSanitizer){

  }
  modalRef: MdbModalRef<AddEditEmployeeComponent> | null = null
  emplist: any = [];
  emp: any;
  modalTitle: any = "";
  ngOnInit(): void{
    this.refreshEmpList();
  }
  refreshEmpList(){
    this.loader.show();
      this.apiService.GetEmployeeList().subscribe(res =>{
        this.emplist = res;
        res.forEach(resEle => {
          if(resEle.profilePicture!="saved"){
            resEle.picFile = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' 
          + resEle.profilePicture);

          }
        });
        
        this.loader.hide();
      },
      error => {
        this.toast.error(error,"Error!!!");
        this.loader.hide();
      }
    )
  }
  addClick(){
    this.emp = {
      employeeId: 0,
      employeeName: "",
      departmentId: 0,
      departmentName: "",
      doj: formatDate(new Date, 'yyyy-MM-dd', 'en-US'),
      profilePicture: ""
    };
    this.modalTitle = "Add Employee";
    this.modalRef = this.modal.open(AddEditEmployeeComponent,{
      data: {
        emp: this.emp,
        modalTitle: this.modalTitle
      }
    });
    this.modalRef.onClose.subscribe(()=>{
      this.refreshEmpList();
    });
  }
  editClick(item: any){
    this.emp = item;
    this.emp.doj = formatDate(this.emp.doj, 'yyyy-MM-dd', 'en-US');
    this.modalTitle = "Edit Employee";
    this.modalRef = this.modal.open(AddEditEmployeeComponent,{
      data: {
        emp: this.emp,
        modalTitle: this.modalTitle
      }
    });
    this.modalRef.onClose.subscribe(()=>{
      this.refreshEmpList();
    });
  }
  deleteClick(employeeId: any){
    this.loader.show();
    this.apiService.DeleteEmployee(employeeId).subscribe(res =>{
      this.toast.success(res, "Deleted Successfully");
      this.refreshEmpList();
      this.loader.hide();
    },
    error =>{
      this.toast.success(error, "Error!!!");
      this.loader.hide();
    }
    )
  }


}
