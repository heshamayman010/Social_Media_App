import { Component, inject, OnInit } from '@angular/core';
import { MebmerServiceService } from '../../_Services/mebmer-service.service';
import { HttpClient } from '@angular/common/http';
import { Member } from '../../_models/Member';
import { MemeberCardComponent } from "../memeber-card/memeber-card.component";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-members-list',
  standalone: true,
  imports: [MemeberCardComponent,PaginationModule,FormsModule],
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css'
})
export class MembersListComponent implements OnInit {

  memberservice=inject(MebmerServiceService);
  http=inject(HttpClient);

  pagesize=8;
  pagenumber=1;
  ngOnInit(): void {
       this.loadMembers();

  }


  
// function to load all the members

   loadMembers() {

 this.memberservice.getAllMembers(this.pagenumber,this.pagesize);


}


changepage(event:any){

if(this.pagenumber!==event.page)
{
this.pagenumber=event.page;
this.loadMembers();

} 
  
}


}









