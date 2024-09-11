export interface Pagination{


currentpage:number,
itemsperpage:number,
totalitems:number,
totalpages:number,
}

 
export class PaginatedResult<T>{

    items?: T ;
pagination?:Pagination;

orderBy?:string;

}

// pagenumber.pagesize

