import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Toast, ToastrService } from 'ngx-toastr';
import { AccountServiceService } from '../app/_Services/account-service.service';
import { catchError } from 'rxjs';
import { routes } from '../app/app.routes';
import { NavigationBehaviorOptions, NavigationExtras, Router } from '@angular/router';

export const erorrInterceptor: HttpInterceptorFn = (req, next) => {
let toatsr=inject(ToastrService);
const service=inject(AccountServiceService);
const router=inject(Router);

// and because the next reurns observable we can use with it the pipe 
  return next(req).pipe(


    catchError(error=>{

      if(error){

          switch (error.status) {
           
           // this cas contains the most of the work as it may happen due to a lot of reasons 
            case 400:
              
            if(error.error.errors){
            const erorrarray=[]; // this array will be uesd to add  the validation error comes from the validation


              for(const key in error.error.errors){  // that because at this type of erorrs the erorr contains array of key and value of it
                  if(error.erorr.errors[key]){
                erorrarray.push(error.erorr.errors[key])
              }
            }
            throw erorrarray.flat(); // flat is used here to show it as a single array 

            }else{

              
              toatsr.error(error.error,error.status);
            }
              break;
          
          case 401:

          toatsr.error(error.error,error.status);
           break;


           case 404 :
                router.navigateByUrl('/not-found');
           break;
          
           case 500:
            let extras:NavigationExtras={state:{error:error.error}}
            router.navigateByUrl('/server-erorrs',extras);
            break;
          
          
          default:
            toatsr.error("unknown erorr happend")



            break;
          }
        
      }
      throw error;

    })
  );




};
