import { Component, inject, OnInit } from '@angular/core';
import { MebmerServiceService } from '../../_Services/mebmer-service.service';
import { HttpClient } from '@angular/common/http';
import { Member } from '../../_models/Member';
import { MemeberCardComponent } from "../memeber-card/memeber-card.component";

@Component({
  selector: 'app-members-list',
  standalone: true,
  imports: [MemeberCardComponent],
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.css'
})
export class MembersListComponent implements OnInit {

  memberservice=inject(MebmerServiceService);
  http=inject(HttpClient);

  ngOnInit(): void {
    if(this.memberservice.members().length===0) this.loadMembers();

  }


  
// function to load all the members

   loadMembers() {

 this.memberservice.getAllMembers();
}
}









