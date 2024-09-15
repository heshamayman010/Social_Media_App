using System;

namespace API.Dtos;

public class MessagesDto
{



public int Id { get; set; }
public int SenderId { get; set; }
public required string SenderUserName {set;get;}
public required string SenderPhotoUrl{get;set;}
public required string RecipientPhotoUrl{get;set;}
public int RecipientId { get; set; }
public required string RecipientUserName {set;get;}
public required string Content {set;get;}
public DateTime DateReadd {set;get;}
public DateTime MessageSent {set;get;}

}
