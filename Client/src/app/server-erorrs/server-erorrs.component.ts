import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-erorrs',
  standalone: true,
  imports: [],
  templateUrl: './server-erorrs.component.html',
  styleUrl: './server-erorrs.component.css'
})
export class ServerErorrsComponent {
error: any;

// in this component we must use the constructor to be able to recieve the data that was sent using the navigate 

constructor(private router:Router){
// then we will recieve the extras data 

const navigate=router.getCurrentNavigation();

this.error=navigate?.extras?.state?.['error'];


}

}
