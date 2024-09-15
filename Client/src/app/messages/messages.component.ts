import { Component, inject, OnInit } from '@angular/core';
import { MessagesService } from '../_Services/messages.service';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { FormsModule } from '@angular/forms';
import { messages } from '../_models/messages';
import { RouterLink } from '@angular/router';
import { TimeagoModule } from 'ngx-timeago';
@Component({
  selector: 'app-messages',
  standalone: true,
  imports: [ButtonsModule,FormsModule,RouterLink,TimeagoModule],
  templateUrl: './messages.component.html',
  styleUrl: './messages.component.css'
})
export class MessagesComponent implements OnInit {
ngOnInit(): void {

  this.loaddata();
}

messageservice=inject(MessagesService);
container="UnRead"
pagenumber=1;
pagesize=5;


loaddata(){

this.messageservice.getMessages(this.pagenumber,this.pagesize,this.container);

}

changepage(event:any){

if(this.pagenumber!==event.page)
{
this.pagenumber=event.page;
this.loaddata();

} 
  
}


getroute(mess:messages){

if(this.container==="OutBox" )return `/members/${mess.recipientUserName}`;else{

  return `/members/${mess.senderUserName}`
}

}


deletemessage(id :number){

  this.messageservice.deleteMessage(id).subscribe({
    next:()=>this.messageservice.returneddata.update(
      prev=>{
        if(prev&&prev.items){
          prev.items.splice(prev.items.findIndex(x=>x.id===id),1);
        return prev

        }
return prev;

    })
  })
}


}
