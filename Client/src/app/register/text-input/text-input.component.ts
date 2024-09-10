import { NgIf } from '@angular/common';
import { Component, input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  standalone: true,
  imports: [NgIf,ReactiveFormsModule],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.css'
})
export class TextInputComponent implements ControlValueAccessor {
  // the work here is done using the constructor
  // self here is used to make the service provid istance for each one as the angular by default work with singletone
  constructor(@Self() public Ngcontrol:NgControl  ) {
    this.Ngcontrol.valueAccessor=this;

    
  }
  
  type=input<string>('type');
  label=input<string>('');
  
  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }
  setDisabledState?(isDisabled: boolean): void {
  }



  get control():FormControl{

return this.Ngcontrol.control as FormControl
  }
}
