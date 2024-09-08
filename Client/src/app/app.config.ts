import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import{provideAnimations} from'@angular/platform-browser/animations';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideToastr, Toast } from 'ngx-toastr';
import { erorrInterceptor } from '../interceptors/erorr.interceptor';
import { jWtInterceptor } from '../interceptors/jwt.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes)
,
// and we will use the provide httpclient to be able to make api calls 
provideHttpClient(withInterceptors([jWtInterceptor,erorrInterceptor]))
,provideAnimations(),
provideToastr(

{

  positionClass:'toast-bottom-center'
}

),
provideRouter(routes)
  ]
};
