using System;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers;

public class PageList<T>:List<T>
{
public PageList(IEnumerable<T>items,int count ,int Pagenumber , int pageSize)
{

    Currentpage=Pagenumber;
    Totalpages= (int)Math.Ceiling(count/(double)Pagesize);
    Pagesize=pageSize;

    Totalcount=count;

    AddRange(items);
}


public int Currentpage { get; set; }
public int Totalpages { get; set; }
public int Pagesize { get; set; }
public int Totalcount { get; set; }

public static async Task<PageList<T>>CreateAsync(IQueryable<T> query,int PageNumber,int PageSize)
{
var count=await query.CountAsync();

var items=await query.Skip((PageNumber-1)*PageSize).Take(PageSize).ToListAsync();
return new PageList<T>(items,count,PageNumber,PageSize);
}

}