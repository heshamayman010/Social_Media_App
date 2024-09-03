import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountServiceService } from '../_Services/account-service.service';
import { ToastrService } from 'ngx-toastr';

export const auhtGuard: CanActivateFn = (route, state) => {

let accountservice=inject(AccountServiceService);
let toastr=inject(ToastrService);


if(accountservice.currentuser()){

  return true;
}
else{

  toastr.error("you are not allowed to enter this route")

  return false;
}

};
