import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../memebers/member-edit/member-edit.component';

export const preventUnsavedChangessGuard: CanDeactivateFn<MemberEditComponent> = (component, ) => {
  
  if(component.myform?.dirty){

   return confirm("if you left the page all changes will be lost ")
  }
  
  
  return true;
};
