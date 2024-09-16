import { Component, computed, inject, input } from '@angular/core';
import { Member } from '../../_models/Member';
import { RouterLink } from '@angular/router';
import { LikesService } from '../../_Services/likes.service';
import { MebmerServiceService } from '../../_Services/mebmer-service.service';
import { PresencehubService } from '../../_Services/presencehub.service';

@Component({
  selector: 'app-memeber-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './memeber-card.component.html',
  styleUrl: './memeber-card.component.css'
})

// this class is mainly used to show the user data in card 
export class MemeberCardComponent {
mylikeservice=inject(LikesService);
usertoshow=input.required<Member>();
mymemberservice=inject(MebmerServiceService)

myhubservice=inject(PresencehubService);
Isonline=computed(()=>
{
 return this.myhubservice.onlineusers().includes(this.usertoshow().userName) // this function will return boolean 
})


// computed is signal that is used when you want to work with another signals datat and it takes function 
//as parameter 
hasLiked=computed(
  ()=>this.mylikeservice.likedIds().includes(this.usertoshow().id)
)


toggleLike(){
this.mylikeservice.toggleLike(this.usertoshow().id).subscribe(
{// here in the function we will check if the user is already liked or not if liked and press again the like will be 
  // removed and if hasnt like the like will be added and his id will be added to thel is of  the id 
  next:()=>{
    if(this.hasLiked()){     
      this.mylikeservice.likedIds.update(c=>c.filter(p=>p!=this.usertoshow().id))  
    }
else{

  this.mylikeservice.likedIds.update(c=>[...c,this.usertoshow().id])
}
  }
}

)

}
}





