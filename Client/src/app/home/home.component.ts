import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';
import { AccountServiceService } from '../_Services/account-service.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
ngOnInit(): void {
this.getuser()
this.currentuser();


}
http=inject(HttpClient);
  users: any;
  service=inject(AccountServiceService);


registermode=false;

registertoggle(){

  this.registermode=!this.registermode;
}


getuser()
{
 this.http.get(`http://localhost:5000/api/user`).subscribe(

      {
      next:response=>this.users=response,
      error:error=>console.log(`${error}`),
      complete:()=>console.log("you finished the call successfully")
      }
    )
}

currentuser(){
const userdata=localStorage.getItem('user');
if(!userdata)return;
let userobject=JSON.parse(userdata);
this.service.currentuser.set(userobject)

}
cancleregisterathome(event: boolean) {

this.registermode=event;

}


}
