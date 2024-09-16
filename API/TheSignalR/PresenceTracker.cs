using System;

namespace API.TheSignalR;

public class PresenceTracker
{

// here we will store the user name and the list of the users id 
public static readonly  Dictionary<string,List<string>> OnlineUsers=[];


public Task Userconnected(string username,string connectionid){
// here we will user the look to prevent any other thing from modifing the dictionary 
lock(OnlineUsers){
if(OnlineUsers.ContainsKey(username)){
OnlineUsers[username].Add(connectionid);
}else{

    OnlineUsers.Add(username,new List<string>{connectionid});
}
}
return Task.CompletedTask;
}



public Task UserDisconnected(string username,string connectionid){
// here we will user the look to prevent any other thing from modifing the dictionary 
lock(OnlineUsers){
if(!OnlineUsers.ContainsKey(username)) return Task.CompletedTask;


    OnlineUsers[username].Remove(connectionid);

// then we need to check if the user is opening from another device 
if(OnlineUsers[username].Count==0){

    OnlineUsers.Remove(username);
}}
return Task.CompletedTask;



}




public Task<string[]> GetAlltheOnlineUsers(){

string[] theonlineusers;
lock(OnlineUsers){

    theonlineusers=OnlineUsers.OrderBy(c=>c.Key).Select(x=>x.Key).ToArray();
}
// here it is used to return completed task with speciefied result 
return Task.FromResult(theonlineusers);


}




}
