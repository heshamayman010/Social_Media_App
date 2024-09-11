using System;

namespace API.Helpers;
// this class is only used to get the data or parameters from the users and also to make limits for the user inputs 
public class UserParameters
{

private const int MaxPageSize=20;

public int pagenumber { get; set; }=1;

private int _pagesize =10;


public int pagesize {

    set=>_pagesize=(value >_pagesize)? _pagesize:value ;
    get=>_pagesize;
}

public string? currenusername{set;get;}
// public string? OrderBy{set;get;}="LAstActiv";


}
