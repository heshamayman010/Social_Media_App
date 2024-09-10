import { Component, EventEmitter, inject, input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountServiceService } from '../_Services/account-service.service';
import { ToastrService } from 'ngx-toastr';
import { JsonPipe, NgIf } from '@angular/common';
import { TextInputComponent } from "./text-input/text-input.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, JsonPipe, NgIf, TextInputComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  ngOnInit(): void {
this.intializeform();
  }
private router=inject(Router);
  // when using the reactive form we are manging the forms controls via the class component it self 
  registerform:FormGroup=new FormGroup({});
  validationerorrArray:string[]|undefined;


  toastr=inject(ToastrService)
service=inject(AccountServiceService);
  cancle() {

this.cancletheregister.emit(false);

}
@Output() cancletheregister=new EventEmitter();


resgister() {
this.service.Register(this.registerform.value).subscribe({
  
  next:_nothing=>
this.router.navigateByUrl('/members')
  ,
  error:error=>{
    this.validationerorrArray=error;
  }});
this.cancle()

}

fb=inject(FormBuilder);
// now lets make our custom validator 

comparepassword(matchto:string):ValidatorFn{

  // here it returns function 
  return (compare:AbstractControl)=>{

   return compare.value===compare.parent?.get(matchto)?.value ? null :{IsMatch:true};
  }
}

intializeform(){

this.registerform=this.fb.group(
{
  gender:['male',Validators.required],
username:['',[Validators.maxLength(20),Validators.required]],
password:['',[Validators.required,Validators.maxLength(50),,Validators.minLength(8)]],
comparepassword:['',[Validators.required,this.comparepassword('password'),Validators.minLength(8)]] ,
  knownAs:['',Validators.required],
  dateOfBirth:['',],
  city:['',Validators.required],
  country:['',Validators.required],
}
// this part is for the change of the password at the password compare process
)
this.registerform.controls['password'].valueChanges.subscribe(
  {
next:()=>{
  this.registerform.controls['comparepassword'].updateValueAndValidity();
}
  }
)
}
}
