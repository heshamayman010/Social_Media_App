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
messages=input<messages[]>([])
myaccountservice=inject(AccountServiceService);
messagestest:messages[]=[];
sendmessage(){
this.messageservice.sendmessage(this.username(),this.messagecontent).subscribe({
next:_=>this.loaddata(),
error:e=>console.log(e)  
})

}

// this was the old way before enabling it from the tabs activation and will be deleted 
loaddata(){
// this was the old way before enabling it from the tabs activation and will be deleted 
this.messageservice.getmessagesthread(this.username()).subscribe({
next:data=>{
this.messagestest=data,
this.messageform?.reset();
},
error:e=>console.log(e)  
})


}



}
