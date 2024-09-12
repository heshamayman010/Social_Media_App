import { HttpClient } from '@angular/common/http';
import { inject, Injectable, model, signal } from '@angular/core';
import { map, Observable } from 'rxjs';
import { User } from '../_models/user';
import { LikesService } from './likes.service';

@Injectable({
  providedIn: 'root'
})
export class AccountServiceService {

// here we will use the like service to make the get all likes function works after the login 
mylikeservice=inject(LikesService);

// to create signal 
 currentuser=signal<User|null>(null);

   basuerl="http://localhost:5000/api/"
  private http=inject(HttpClient);

// the following part is for retrieving the data from the local storage if the user is already loged in

constructor() {
    this.loadUserFromStorage(); // Load user from localStorage when service is created
  }

  // Load user from localStorage when the app starts
  private loadUserFromStorage() {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString) as User;
      this.currentuser.set(user);
    }
  }





  login(model:any){

    return this.http.post<User>(this.basuerl+"Account/login",model).pipe(
      map(user=>{
        if(user){
          this.setcurrentuser(user);
        }
      })
    )
  }


  Register(model:any){
// using the map here is for makin the user stay persistant 
    return this.http.post<User>(this.basuerl+"Account/register",model).pipe(
      map(user=>{
        if(user){
          this.setcurrentuser(user);
        }
     return user;
      }
    )
    )
  }

logout(){

localStorage.removeItem('user');
this.currentuser.set(null);


}


setcurrentuser(user:User){

localStorage.setItem('user',JSON.stringify(user));
      this.currentuser.set(user);
      this.mylikeservice.getlikeIds();


}



}
