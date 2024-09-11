import { Component, ElementRef, inject, input, output, signal, ViewChild } from '@angular/core';
import { Member } from '../../_models/Member';
import { AccountServiceService } from '../../_Services/account-service.service';
import { MebmerServiceService } from '../../_Services/mebmer-service.service';
import { Photo } from '../../_models/Photo';
import { Router } from '@angular/router';


@Component({
  selector: 'app-photo-editor',
  standalone: true,
  imports: [],
  templateUrl: './photo-editor.component.html',
  styleUrl: './photo-editor.component.css'
})
export class PhotoEditorComponent {
member=input.required<Member>();

private accountservice=inject(AccountServiceService);
private memberservice=inject(MebmerServiceService);
router=inject(Router);
memberchange=output<any>()

anothermemberchange=output<any>()

SetmainPhoto(photo :Photo){
this.memberservice.setmaiphoto(photo).subscribe(
  {next:_none=>{
// here we can change the photo of the current user  

const user=this.accountservice.currentuser();
if(user){
user.photpUrl=photo.url;
this.accountservice.currentuser.set(user);
}

// {} here is used as we are spreading an object not array 
// this variable which will be sent to the parent component 
const updatedmember={...this.member()}

updatedmember.photoUrl=photo.url;

updatedmember.photos.forEach(
p=>{

  if(p.isMain) p.isMain=false;
  if(p.id===photo.id) p.isMain=true;
}
)
this.memberchange.emit(updatedmember)
  }
  }
)






}


deletephoto(photoid:number){

// this.memberservice.deletephot(photoid).subscribe({
// next:_nothing=>{
//   const updatedmember={...this.member()};
//   updatedmember.photos=updatedmember.photos.filter(x=>x.id!==photoid);
  
//   this.memberchange.emit(updatedmember);

window.location.reload();
}

}
  
// }

// }



// another way instead of the ng2file upload 


// imageName = signal('');
//   fileSize = signal(0);
//   uploadProgress = signal(0);
//   imagePreview = signal('');
//   @ViewChild('fileInput') fileInput: ElementRef | undefined;
//   selectedFile: File | null = null;
//   uploadSuccess: boolean = false;
//   uploadError: boolean = false;


//   // Handler for file input change
//   onFileChange(event: any): void {
//     const file = event.target.files[0] as File | null;
//     this.uploadFile(file);
//   }

//   // Handler for file drop
//   onFileDrop(event: DragEvent): void {
//     event.preventDefault();
//     const file = event.dataTransfer?.files[0] as File | null;
//     this.uploadFile(file);
//   }

//   // Prevent default dragover behavior
//   onDragOver(event: DragEvent): void {
//     event.preventDefault();
//   }

//   // Method to handle file upload
//   uploadFile(file: File | null): void {
//     if (file && file.type.startsWith('image/')) {
//       this.selectedFile = file;
//       this.fileSize.set(Math.round(file.size / 1024)); // Set file size in KB

//       const reader = new FileReader();
//       reader.onload = (e) => {
//         this.imagePreview.set(e.target?.result as string); // Set image preview URL
//       };
//       reader.readAsDataURL(file);

//       this.uploadSuccess = true;
//       this.uploadError = false;
//       this.imageName.set(file.name); // Set image name
//     } else {
//       this.uploadSuccess = false;
//       this.uploadError = true;
//       // // this.snackBar.open('Only image files are supported!', 'Close', {
//       //   duration: 3000,
//       //   panelClass: 'error',
//       // });
//     }
//   }

//   // Method to remove the uploaded image
//   removeImage(): void {
//     this.selectedFile = null;
//     this.imageName.set('');
//     this.fileSize.set(0);
//     this.imagePreview.set('');
//     this.uploadSuccess = false;
//     this.uploadError = false;
//     this.uploadProgress.set(0);
//   }




