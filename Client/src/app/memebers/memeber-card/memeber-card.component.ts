import { Component, input } from '@angular/core';
import { Member } from '../../_models/Member';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-memeber-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './memeber-card.component.html',
  styleUrl: './memeber-card.component.css'
})

// this class is mainly used to show the user data in card 
export class MemeberCardComponent {

usertoshow=input.required<Member>();


}
