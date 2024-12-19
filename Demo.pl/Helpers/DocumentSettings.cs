using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.pl.Helpers
{
    public class DocumentSettings
    {

        public static  string UploadFile(IFormFile file , string FolderName)
        {

            //1.Get Located Folder Path
            //string FolderPath = "C:\\Users\\20100\\Desktop\\Assignement_7_MVC\\Demo.pl\\Demo.pl\\wwwroot\\Files\\" + FolderName;
            //string FolderPath=Directory.GetCurrentDirectory() + "C:\\Users\\20100\\Desktop\\Assignement_7_MVC\\Demo.pl\\Demo.pl\\wwwroot\\" + FolderName;

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files\\", FolderName);

            //2.Get File Name And Make It Uniqe

            string fileName=$"{Guid.NewGuid()}{file.FileName}";  //Make It Uniqe

            //3.Get File Path [ folder path + file name]
            
            string filePath= Path.Combine(folderPath,fileName);

            //4.save file as streams

          using var FileStream = new FileStream(filePath,FileMode.Create);

            file.CopyTo(FileStream);

            return fileName;

        }


        public static void DeleteFile(string fileName , string FolderName)
        {
            //1. Get File Path

            string FilePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files\\", FolderName, fileName);

            //2.check if file Exists  or not if exists remove it

            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
