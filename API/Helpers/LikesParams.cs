using System;

namespace API.Helpers;

public class LikesParams:PaginationParams
{


public int UserId { get; set; }


public string Predicate{set;get;}="liked";

}
