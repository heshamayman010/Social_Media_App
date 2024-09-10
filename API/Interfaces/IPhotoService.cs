using System;
using CloudinaryDotNet.Actions;

namespace API.Interfaces;

public interface IPhotoService
{
// here the ImageUploadResult is in the cloudinary dotnet itself 
Task<ImageUploadResult>AddPhoto(IFormFile photo);


Task<DeletionResult>DeletePhoto(string publicid);
}
