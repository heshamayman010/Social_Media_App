using System;

namespace API.Helpers;

public class PaginationParams
{





private const int MaxPageSize=20;

public int pagenumber { get; set; }=1;

private int _pagesize =10;


public int pagesize {

    set=>_pagesize=(value >_pagesize)? _pagesize:value ;
    get=>_pagesize;
}

}
