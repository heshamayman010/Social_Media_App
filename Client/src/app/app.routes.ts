import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { MembersListComponent } from './memebers/members-list/members-list.component';
import { MembersDataComponent } from './memebers/members-data/members-data.component';
import { ListsComponent } from './lists/lists.component';
import { auhtGuard } from './_guards/auht.guard';

export const routes: Routes = [

{path:'',component:HomeComponent},

{path:'',
canActivate:[auhtGuard],
runGuardsAndResolvers:'always',
children:[
{path:'messages',component:MessagesComponent},
{path:'members',component:MembersListComponent},
{path:'members/:id',component:MembersDataComponent},
{path:'lists',component:ListsComponent},
]


}
,

{path:'**',component:HomeComponent,pathMatch:'full'}




];
