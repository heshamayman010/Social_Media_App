using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
[Table("LikeUser")]
public class LikeUser
{
public int SourceUserId { get; set; }
public AppUser SourcUser{set;get;}=null!;


public int TargetUserId { get; set; }
public AppUser TargetUser {set;get;}=null!;


}