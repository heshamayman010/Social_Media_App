import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AccountServiceService } from '../_Services/account-service.service';
import{BsDropdownModule} from'ngx-bootstrap/dropdown'
import {  Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,BsDropdownModule,RouterLink, RouterLinkActive],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {

router=inject(Router);
  model:any={};
 public accountservice=inject(AccountServiceService);
public logedin=false;
toastr=inject(ToastrService);

login(){
// the return of the httpclient method is obsservable so we must make subscripe to make it excuted 
this.accountservice.login(this.model).subscribe({

  next: () => {
this.router.navigateByUrl('/messages')
  },
  // error:error=>console.log(error)  this is the old way 

  error:error=>this.toastr.error(error.error)
})
}



logout(){
  this.router.navigateByUrl('/');

this.accountservice.logout(

)}
}
