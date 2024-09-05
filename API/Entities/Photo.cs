using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
[Table("Photos")]
public class Photo
{
public int Id { get; set; }
public required string Url { get; set; }
public  string? PublicId  { get; set; }
public bool IsMain { get; set; }


//for user 

public int AppUserId {set;get;}

public AppUser appUser{set;get;}=null!;

}