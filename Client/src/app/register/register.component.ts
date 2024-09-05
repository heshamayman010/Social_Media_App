import { Component, EventEmitter, inject, input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountServiceService } from '../_Services/account-service.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  validationerorrArray:string[]=[];


  toastr=inject(ToastrService)
service=inject(AccountServiceService);
  cancle() {

this.cancletheregister.emit(false);

}


@Output() cancletheregister=new EventEmitter();

model:any={};

resgister() {

  if(this.model.username==""){ this.toastr.error("username cant be empty ")}
  if(this.model.password==""){ this.toastr.error("password cant be empty ")}
    
this.service.Register(this.model).subscribe({


  next:response=>{

    console.log(response);
  },
  error:error=>{
    this.validationerorrArray=error;
  }


});


this.cancle()
}

}
