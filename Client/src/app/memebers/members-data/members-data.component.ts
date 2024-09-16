import { Component, inject, OnInit, ViewChild, viewChild } from '@angular/core';
import { MebmerServiceService } from '../../_Services/mebmer-service.service';
import { ActivatedRoute, RouterLinkActive } from '@angular/router';
import { Member } from '../../_models/Member';
import { TabDirective, TabsetComponent, TabsModule } from 'ngx-bootstrap/tabs';
// import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { map } from 'rxjs';
import { DatePipe } from '@angular/common';
import { MemberMessagesComponent } from "../member-messages/member-messages.component";
import { messages } from '../../_models/messages';
import { PresencehubService } from '../../_Services/presencehub.service';
import { MessagesService } from '../../_Services/messages.service';


@Component({
  selector: 'app-members-data',
  standalone: true,
  imports: [TabsModule, DatePipe, MemberMessagesComponent],
  templateUrl: './members-data.component.html',
  styleUrl: './members-data.component.css'
})

// this class to show the data of specifec user
export class MembersDataComponent implements OnInit {
// Images:GalleryItem[]=[];
messageservice=inject(MessagesService)
myhubservice=inject(PresencehubService);
  private service=inject(MebmerServiceService);
member?:Member
// and this activate repute will be used to take the snapshots and give back the dat we want 
rout=inject(ActivatedRoute);

@ViewChild("membertabs") membertabs?:TabsetComponent;
activetab?:TabDirective;  // this will be used to know which is the active tap now 
messages:messages[]=[];
ngOnInit(): void {
this.loaduserdata();
// the next one will be used when the user press the messages button at the card that will check for it s query params to send him to the messages taba
this.rout.params.subscribe({
next:params=>{

  params["tab"] &&this.SelectTab(params["tab"])
} 

})
}

// now function to load the data if it is the active tap 
OnTabActivated(data:TabDirective)
{
this.activetab=data;
// Then make checks 
if(this.activetab.heading==="Messages"&&this.messages.length===0&&this.member){
this.messageservice.getmessagesthread(this.member.userName).subscribe({
next:data=>{
this.messages=data}})
}

};

SelectTab(heading:string){
// this function will be used to make the messages button select the messaging tab 
// it will mainly check if pressed will take the heading string from the button and find it is tap and make it active 
  if(this.membertabs){
const messagetab=this.membertabs?.tabs.find(x=>x.heading===heading)

if(messagetab)messagetab.active=true;  // this will switch it to the messages tab 
  }
}



 loaduserdata(){

  const username=this.rout.snapshot.paramMap.get("username")
  if(!username)return;

  this.service.getMemberByusername(username).subscribe({
  
      next:mem=>
      {        this.member=mem;
        mem.photos.map(x=>
{
          // this.Images.push(new ImageItem({src: x.url,thumb:x.url}))

}
        )
}
    })

}

}
