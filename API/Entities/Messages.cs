using System;

namespace API.Entities;

public class Messages
{

public int Id { get; set; }
public required string SenderUserName {set;get;}
public required string RecipientUserName {set;get;}
public required string Content {set;get;}
public DateTime? DateReadd {set;get;}
public DateTime? MessageSent {set;get;}=DateTime.UtcNow;
public bool? SenderDeleteted {set;get;}=false;
public bool? RecipientDeleteted {set;get;}=false;


// and for the navigation propirties 

public required AppUser Sender {set;get;}=null!;
public int SenderId { get; set; }
public int RecipientId { get; set; }
public required AppUser Recipient {set;get;}=null!;


}
