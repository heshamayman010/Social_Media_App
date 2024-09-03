import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AccountServiceService } from '../_Services/account-service.service';
import{BsDropdownModule} from'ngx-bootstrap/dropdown'
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
model:any={};
 public accountservice=inject(AccountServiceService);
public logedin=false;

login(){
// the return of the httpclient method is obsservable so we must make subscripe to make it excuted 
this.accountservice.login(this.model).subscribe({

  next: respone => {
console.log(respone);

  },
  error:error=>console.log(error)
})
}



logout(){

this.accountservice.logout(

)}
}
