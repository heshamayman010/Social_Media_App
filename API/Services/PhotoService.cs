using System;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services;

public class PhotoService : IPhotoService
{

private readonly Cloudinary _cloudinary;

// first we need to inject the cloudinary ooptions 
public PhotoService(IOptions<CloudinarySettings> config)
{
    // here we specefiy the data of the cloud 
var acc=new Account(config.Value.CloudName,config.Value.ApiKey,config.Value.ApiSecret);

// then we create object of the class cloudinary with the account data we provided 
_cloudinary=new Cloudinary(acc);
}

    public async Task<ImageUploadResult> AddPhoto(IFormFile photo)
    {

        var Uploadresult=new ImageUploadResult();

        if(photo.Length>0){
            
            // then we had to open stream to read the photo 
            using var stream=photo.OpenReadStream();

            // then we will create the upload params which will hold the nessacary parms while uploading the photo to the cloudinary 
            var uploadparams=new ImageUploadParams(){

                // here the stream object contains the actual photo data 
        File =new FileDescription(photo.FileName,stream),

        // and here we are able to control the styling of the phot 

        Transformation=new Transformation().Height(500).Width(500).Gravity("face").Crop("fill"),

        // and here we will create folder for the imags 
        Folder="da-net8"
            };


// here you must assign the result of the cloudinary to it 
Uploadresult=await _cloudinary.UploadAsync(uploadparams);
        }
return Uploadresult;
    }



    public async Task<DeletionResult> DeletePhoto(string publicid)
    {

        var deleteparams=new DeletionParams(publicid);

        return  await _cloudinary.DestroyAsync(deleteparams);
    }
}
