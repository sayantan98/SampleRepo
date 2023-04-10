import { Component } from '@angular/core';
import { MdbModalRef,MdbModalService } from 'mdb-angular-ui-kit/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { SharedService } from 'src/app/shared.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css']
})
export class AddEditEmployeeComponent {
  public emp: any;
  public depList: any = [];
  public modalTitle: string='';
  
  constructor(private apiService:SharedService,public modal: MdbModalRef<AddEditEmployeeComponent>, private toast: ToastrService, private loader: NgxSpinnerService){
  }
  ngOnInit() : void{
    this.getDepList();
  }
  getDepList(){
    this.apiService.GetDepartmentList().subscribe(res=>{
        this.depList=res;
    }
    )
  }
  saveClick(heroForm: NgForm){
    this.loader.show();
    if(heroForm.valid){
        this.emp.profilePicture = "saved";


        this.apiService.InsertEmployee(this.emp).subscribe(res=>{
          this.toast.success(res,"Success");
          this.modal.close();
          this.loader.hide();
        },
        error => {
          this.toast.error(error.statusText,"Error");
          this.modal.close();
          this.loader.hide();
        });
    }
    else{
      this.toast.warning("Please fill all the fields.","Warning!!");
    }
  }
  uploadFiles(event: any){
    let profile: File = event.target.files[0];
    let extFile = this.emp.profilePicture.substring(this.emp.profilePicture.lastIndexOf('.') + 1);
    if(extFile =="jpeg"){
      const formData = new FormData();
      formData.append('file', profile, this.emp.employeeName + '-' + this.emp.doj);
      this.apiService.UploadFiles(formData).subscribe(res=>{
        this.toast.success(res,"Success");
        this.loader.hide();
      },
      error => {
        this.toast.error(error.statusText,"Error");
        this.loader.hide();
      });
    }
    else{
      this.toast.warning("Profile Picture should be jpeg.","Warning!!");
      this.emp.profilePicture = "";
    }
  }
  updateClick(heroForm: NgForm){
    this.loader.show();
    if(heroForm.valid){
      let extFile = this.emp.profilePicture.substring(this.emp.profilePicture.lastIndexOf('.') + 1);
        this.emp.profilePicture = this.emp.employeeName + '-' + this.emp.doj + "."+ extFile;


        this.apiService.UpdateEmployee(this.emp).subscribe(res=>{
          this.toast.success(res,"Success");
          this.modal.close();
          this.loader.hide();
        },
        error => {
          this.toast.error(error.statusText,"Error");
          this.modal.close();
          this.loader.hide();
        });
    }
    else{
      this.toast.warning("Please fill all the fields.","Warning!!");
    }
  }

}
