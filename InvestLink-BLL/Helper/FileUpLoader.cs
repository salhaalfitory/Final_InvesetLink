using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Helper
{
    public class FileUpLoader
    {
        public static string UploaderFile(IFormFile file, string FolderName)
        {
            try
            {
                //step1:GetDirectory
                string FolderPath = Directory.GetCurrentDirectory() + @"\wwwroot\Files\" + FolderName;
                //step2:GetFileName
                string FileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
                //step3:merge
                string FinalFilePath = Path.Combine(FolderPath, FileName);
                //step4:stream
                using (var stream = new FileStream(FinalFilePath, FileMode.Create))
                {
                    file.CopyTo(stream);

                }
                return FileName;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }

        }
    }
}




            //try
            //{
            //    //step1:GetDirectory
            //    string FolderPath = Directory.GetCurrentDirectory() + @"\wwwroot\Files\" + FolderName;
            //    //step2:GetFileName
            //    string FileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
            //    //step3:merge
            //    string FinalFilePath = Path.Combine(FolderPath, FileName);
            //    //step4:stream
            //    using (var stream = new FileStream(FinalFilePath, FileMode.Create))
            //    {
            //        file.CopyTo(stream);

            //    }
            //    return FileName;
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;

            //}


//        }
//    }
//}
