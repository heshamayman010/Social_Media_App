import { Component, inject, input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MessagesService } from '../../_Services/messages.service';
import { messages } from '../../_models/messages';
import { FormsModule, NgForm } from '@angular/forms';
import { AccountServiceService } from '../../_Services/account-service.service';

@Component({
  selector: 'app-member-messages',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './member-messages.component.html',
  styleUrl: './member-messages.component.css'
})
export class MemberMessagesComponent implements OnInit,OnDestroy {
ngOnDestroy(): void {
this.messageservice.StopConnection()

}
ngOnInit(): void {

  this.loaddata();
}
// this class is used to recieve the input from the messages service thread 

@ViewChild("messageform") messageform?:NgForm;
messageservice=inject(MessagesService)
username=input.required<string>();  // the input from the parent component 
messagecontent="";
messages:messages[]=[];
myaccountservice=inject(AccountServiceService);

sendmessage(){
this.messageservice.sendmessage(this.username(),this.messagecontent).subscribe({
next:_=>this.loaddata(),
error:e=>console.log(e)  
})

}


loaddata(){


this.messageservice.getmessagesthread(this.username()).subscribe({
next:data=>{
this.messages=data,
this.messageform?.reset();
},
error:e=>console.log(e)  
})


// the next is for the signal r 
// const user=this.myaccountservice.currentuser();
// if(!user)return;

// // now we will call the message hup functions instead of the api call

// this.messageservice.CreateHubConnection(user,this.username())


}



}
