import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AccountServiceService } from './account-service.service';
import { environment } from '../../environments/environment.development';
import { Member } from '../_models/Member';

@Injectable({
  providedIn: 'root'
})
export class MebmerServiceService {

http=inject(HttpClient);
account=inject(AccountServiceService);

url=environment.ApiUrl;

getAllMembers(){

  // this was the old way before adding the interceptor of jwt 

//  return this.http.get<Member[]>(this.url+ "user",this.getheaderoption())

  return this.http.get<Member[]>(this.url+ "user")
}


getMemberByusername(name:string){

return this.http.get<Member>(this.url+"user/"+name)

}

getMemberById(id:number){

return this.http.get<Member>(this.url+"user/"+id)


// this was the old way before adding the interceptor of jwt 
// return this.http.get<Member>(this.url+"user/"+id,this.getheaderoption())


}




// this function is mainly used to get back the user token and send it with all the request 
getheaderoption(){

return{

headers:new HttpHeaders({
Authorization:`Bearer ${this.account.currentuser()?.toekn}`

})

}



}


}
