import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/Member';

@Injectable({
  providedIn: 'root'
})
export class LikesService {


  url=environment.ApiUrl;
  http=inject(HttpClient);
  likedIds=signal<number[]>([]);
  
// ---------------------- like/

toggleLike(targetuserid:number){
return this.http.post(this.url+"like/"+targetuserid,{});
}
// ----------------
getLikes(predicate:string){

return this.http.get<Member[]>(this.url+"like?predicate="+predicate)
}

// ------------------
// this to get all the like user but i did the function here to retur
getlikeIds(){
return this.http.get<number[]>(this.url+"like/list").subscribe({

  next:data=>this.likedIds.set(data),
      error: err => console.error('Error fetching liked IDs:', err)

})

}




}
