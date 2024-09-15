using System;

namespace API.Helpers;

public class MessagesParams:PaginationParams
{  // this is helper class for creating the inbox

public  string? username { get; set; }

// this to clarify if the messages is read or not 
public string Container {set;get;}="unread";


}
