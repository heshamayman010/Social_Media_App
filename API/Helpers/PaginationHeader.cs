using System;

namespace API.Helpers;

public class PaginationHeader(int Currentpage, int itemsperpage, int totalitems, int totalpages)
{
    public int Currentpage { get; set;} = Currentpage;
    public int Itemsperpage { get;set; } = itemsperpage;
    public int Totalitems { get; set;} = totalitems;
    public int Totalpages { get;set; } = totalpages;
}
