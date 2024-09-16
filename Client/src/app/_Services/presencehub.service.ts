import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ToastrService } from 'ngx-toastr';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class PresencehubService {

huburl=environment.hubsUrl;

onlineusers=signal<string[]>([]);
private toastr=inject(ToastrService);
// then we create object of the hubconnection
private hubconnection?:HubConnection;


CreateHubConnection(user:User){
this.hubconnection=new HubConnectionBuilder()
.withUrl(this.huburl+"presence"
,{
  accessTokenFactory:  ()=>user.token  // this is used to send the token 
  })
.withAutomaticReconnect().build();

this.hubconnection.start().catch(err=>console.log(err))

this.hubconnection.on("UserIsOnline",username=>{

  this.toastr.info(username + " has connected to the server");
})

this.hubconnection.on("UserIsOffline",username=>{

  this.toastr.warning(username + " has left the server");
})

this.hubconnection.on("GetOnLineUsers",userlist=>{

  this.onlineusers.set(userlist);

})



}


StopHubconnection(){

  if(this.hubconnection?.state===HubConnectionState.Connected){

    this.hubconnection.stop().catch(error=>console.log(error))
  }
}





}
