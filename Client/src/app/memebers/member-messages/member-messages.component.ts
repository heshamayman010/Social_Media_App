import { Component, inject, input, OnInit, ViewChild } from '@angular/core';
import { MessagesService } from '../../_Services/messages.service';
import { messages } from '../../_models/messages';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-messages',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './member-messages.component.html',
  styleUrl: './member-messages.component.css'
})
export class MemberMessagesComponent implements OnInit {
ngOnInit(): void {

  this.loaddata();
}
// this class is used to recieve the input from the messages service thread 

@ViewChild("messageform") messageform?:NgForm;
messageservice=inject(MessagesService)
username=input.required<string>();  // the input from the parent component 
messagecontent="";
messages:messages[]=[];


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
}



}
