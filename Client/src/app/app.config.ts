import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import{provideAnimations} from'@angular/platform-browser/animations';
import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { provideToastr, Toast } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes)
,
// and we will use the provide httpclient to be able to make api calls 
provideHttpClient()
,provideAnimations(),
provideToastr(

{

  positionClass:'toast-bottom-center'
}

),
provideRouter(routes)
  ]
};
