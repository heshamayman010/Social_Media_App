import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { AccountServiceService } from './account-service.service';
import { environment } from '../../environments/environment.development';
import { Member } from '../_models/Member';
import { of, tap } from 'rxjs';
import { Photo } from '../_models/Photo';

@Injectable({
  providedIn: 'root'
})
export class MebmerServiceService {

http=inject(HttpClient);
account=inject(AccountServiceService);

url=environment.ApiUrl;
// this signal of members will be used to store the data of the members to make it store data 
members=signal<Member[]>([]);
getAllMembers(){

  // this was the old way before adding the interceptor of jwt 

//  return this.http.get<Member[]>(this.url+ "user",this.getheaderoption())

  return this.http.get<Member[]>(this.url+ "user").subscribe({


    next:data=>this.members.set(data)
  })
}


getMemberByusername(name:string){
  // this will be used to store the data of the user we got from the api call \ 
  const member=this.members().find(x=>x.userName===name);
  // of here is used to return it as observable 
  if(member!==undefined) return  of (member);

return this.http.get<Member>(this.url+"user/"+name)
}
getMemberById(id:number){

return this.http.get<Member>(this.url+"user/"+id)
// this was the old way before adding the interceptor of jwt 
// return this.http.get<Member>(this.url+"user/"+id,this.getheaderoption())
}

Update(member:Member){

return this.http.put(this.url+"user",member);

}

// this function is mainly used to get back the user token and send it with all the request 
getheaderoption(){
return{
headers:new HttpHeaders({
Authorization:`Bearer ${this.account.currentuser()?.token}`
})}
}
// this is the old one but now we want also to make the changes appear instantly 
// setmaiphoto(id:number ){

// return this.http.put(this.url+'user/set-main-photo/'+id,{})

// }

setmaiphoto(photo :Photo){

return this.http.put(this.url+'user/set-main-photo/'+photo.id,{}).pipe(
tap(()=>
  {

    this.members.update(
members=>members.map(m=>{

  if(m.photos.includes(photo)){
    m.photoUrl=photo.url;

  }
  return m;
})

    )

  }


)  
)


}


deletephot(photoid :number){
 return this.http.delete(this.url+'user/delete-photo/'+ photoid)
}

}
