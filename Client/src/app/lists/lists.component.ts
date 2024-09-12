import { Component, inject, OnInit } from '@angular/core';
import { Member } from '../_models/Member';
import { LikesService } from '../_Services/likes.service';
import { FormsModule } from '@angular/forms';
import{ButtonsModule} from 'ngx-bootstrap/buttons'
import { MemeberCardComponent } from "../memebers/memeber-card/memeber-card.component";
@Component({
  selector: 'app-lists',
  standalone: true,
  imports: [FormsModule, ButtonsModule, MemeberCardComponent],
  templateUrl: './lists.component.html',
  styleUrl: './lists.component.css'
})
export class ListsComponent implements OnInit{
ngOnInit(): void {

this.loadLiked();
}
member:Member[]=[];
mylikeservice=inject(LikesService);
predicate:string="liked"  // that is the predicate that will be used to be sent with the url 

// ------------------------------------
// function to load all the users we liked 
loadLiked(){
// console.log(this.predicate)
  this.mylikeservice.getLikes(this.predicate).subscribe({

    next:data=>this.member=data
  })
}


// fucntion to get the title 
gettitle(){

  switch (this.predicate){
  case "liked": return "the members you liked "
  ;case "likedBy" : return "the members who liked you  ";
  default : return "All the Likes "


  }
}


}
