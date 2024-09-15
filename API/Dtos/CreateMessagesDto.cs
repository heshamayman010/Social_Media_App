using System;

namespace API.Dtos;

public class CreateMessagesDto
{
    // this class will be only used to create the messages 

    public required string RecipientUsername{set;get;}
    public required string Content{set;get;}
}
