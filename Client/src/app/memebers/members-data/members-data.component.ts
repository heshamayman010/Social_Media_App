import { Component, inject, OnInit } from '@angular/core';
import { MebmerServiceService } from '../../_Services/mebmer-service.service';
import { ActivatedRoute, RouterLinkActive } from '@angular/router';
import { Member } from '../../_models/Member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { map } from 'rxjs';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-members-data',
  standalone: true,
  imports: [TabsModule,GalleryModule,DatePipe],
  templateUrl: './members-data.component.html',
  styleUrl: './members-data.component.css'
})

// this class to show the data of specifec user
export class MembersDataComponent implements OnInit {
Images:GalleryItem[]=[];
  private service=inject(MebmerServiceService);
member?:Member
// and this activate repute will be used to take the snapshots and give back the dat we want 
rout=inject(ActivatedRoute);


ngOnInit(): void {
this.loaduserdata();
}

 loaduserdata(){

  const username=this.rout.snapshot.paramMap.get("username")
  if(!username)return;

  this.service.getMemberByusername(username).subscribe({
  
      next:mem=>
      {        this.member=mem;
        mem.photos.map(x=>
{
          this.Images.push(new ImageItem({src: x.url,thumb:x.url}))

}
        )
}
    })

}

}
