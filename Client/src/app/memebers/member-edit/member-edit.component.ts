import { Component, HostListener, inject, NgModule, OnInit, ViewChild, viewChild } from '@angular/core';
import { MebmerServiceService } from '../../_Services/mebmer-service.service';
import { AccountServiceService } from '../../_Services/account-service.service';
import { Member } from '../../_models/Member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PhotoEditorComponent } from "../photo-editor/photo-editor.component";

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule, FormsModule, PhotoEditorComponent],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})


export class MemberEditComponent implements OnInit{
@HostListener('window:beforeunload', ['$event']) 


notify($event: any) {
  if (this.myform?.dirty) {
    $event.returnValue = true;  // Corrected to returnValue
  }
}

  ngOnInit(): void {
    this.loadmember();
  }

@ViewChild('myform') myform?:NgForm;
  toastr=inject(ToastrService)
  memberservice=inject(MebmerServiceService);
  account=inject(AccountServiceService);
  member?:Member;
myuser=this.account.currentuser()
  anothermember?:Member;


  loadmember() {

    const user=this.account.currentuser();

    if(!user)return;

    this.memberservice.getMemberByusername(user.userName).subscribe(

      {next:hh=>this.member=hh

      }
    )

  }

  updatemember() {

    this.memberservice.Update(this.myform?.value).subscribe(

{

  next:x=>{    this.toastr.success("the update is successfully ")

    
    this.myform?.reset(this.member)
  }
}

    )
  }



}
