import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountServiceService } from '../app/_Services/account-service.service';

export const jWtInterceptor: HttpInterceptorFn = (req, next) => {
  // here we will work on the request before it is sent to the next 

  const account=inject(AccountServiceService);

  // now the req is emutable so we must make clone of it and send the clone 

  if(account.currentuser()){ // this to check if the user is loged in 
  req=req.clone({
setHeaders:{
Authorization: `Bearer ${account.currentuser()?.toekn}`
}
  })
}
  return next(req);
};
