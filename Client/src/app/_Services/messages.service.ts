import { inject, Injectable, Sanitizer, signal } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { messages } from '../_models/messages';
import { PaginatedResult } from '../_models/Pagination';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {

baseurl=environment.ApiUrl

http=inject(HttpClient);

returneddata=signal<PaginatedResult<messages[]>|null>(null);





getMessages(pagenumber:number, pagesize:number ,container:string){

  let params=this.setPaginationHeaders(pagenumber,pagesize);

  params.append("container",container)


this.http.get<messages[]>(this.baseurl+"messages",{observe:"response",params}).
subscribe({

  next:response=>this.returneddata.set(
{
items:response.body as messages[],
pagination :JSON.parse(response.headers.get('Pagination')!)

}
  )
})



}



getmessagesthread(username:string){

  return this.http.get<messages[]>(this.baseurl+'messages/thread/'+username)
}

sendmessage(username:string,content:string){

  return this.http.post<messages>(this.baseurl+"messages",{recipientUserName:username,content})
 }



private setPaginationHeaders(pagenumber:number,pagesize:number){
let params=new HttpParams();
if(pagenumber&&pagesize){
 params= params.append('pagenumber',pagenumber);
 params= params.append('pagesize',pagesize)
}
return params;
}



deleteMessage(id :number){

return this.http.delete(this.baseurl+"messages/"+id)

}



}
