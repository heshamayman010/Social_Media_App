// this class will be used for creating modal which will be used in the confirmation of edititing 
// nd may be used in another place 

import { Injectable } from '@angular/core';
import{BsModalRef } from'ngx-bootstrap/modal'
@Injectable({
  providedIn: 'root'
})
export class ConfirmService {
BsmodalRef?:BsModalRef;
  constructor() { }
}
