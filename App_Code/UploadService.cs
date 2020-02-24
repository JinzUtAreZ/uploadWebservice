using System;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;

/// <summary>
/// Summary description for WebServiceCS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class UploadService : System.Web.Services.WebService {

    public UploadService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public string Connect()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["EQMConnection"].ToString();
    }

    [WebMethod]
    public void UploadFile(string fileName, string contentType, byte[] bytes)
    {
        //Save the Byte Array as File.
        //string filePath = Server.MapPath(string.Format("~/Uploads/{0}", fileName));
        //File.WriteAllBytes(filePath, bytes);
        
        string errmsg = "" ;
        using (SqlConnection sqlcon = new SqlConnection(Connect()))
        {
            sqlcon.Open();

            try
            {
                SqlCommand sqlcmd = new SqlCommand("spSave_ImageData", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@inFileName", SqlDbType.VarChar).Value = fileName;
                sqlcmd.Parameters.Add("@inType", SqlDbType.VarChar).Value = contentType;
                sqlcmd.Parameters.Add("@inData", SqlDbType.Image, bytes.Length).Value = bytes;
                SqlDataReader sqlrd = sqlcmd.ExecuteReader();

                if (sqlrd.HasRows)
                {
                    while (sqlrd.Read())
                    {
                        errmsg = "Error in loading Service Deploy List";
                    }
                }
            }
            catch (Exception e)
            {
                errmsg = "Error in loading Service Deploy List";
            }
            finally
            {
                sqlcon.Close();
            }

        }
    }

    public class FileData
    { 
        public string FileName;
        public string ContentType;
        public string ImageData;
    }

    [WebMethod]
    public string ViewFile()
    {
        List<FileData> newdt = new List<FileData>();
        string errmsg = "";
        using (SqlConnection sqlcon = new SqlConnection(Connect()))
        {
            sqlcon.Open();

            try
            {
                SqlCommand sqlcmd = new SqlCommand("spLoad_ImageData", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                
                SqlDataReader sqlrd = sqlcmd.ExecuteReader();

                if (sqlrd.HasRows)
                {
                    
                    while (sqlrd.Read())
                    {
                       
                        //Get the byte array from image file
                        //FileName = sqlrd["Base64Data"];
                        byte[] imgBytes = (byte[])sqlrd["Data"];

                        //If you want convert to a bitmap file
                        TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                        Bitmap MyBitmap = (Bitmap)tc.ConvertFrom(imgBytes);

                        //string imgString = Convert.ToBase64String(imgBytes);
                        newdt.Add(new FileData {
                            FileName = sqlrd["ImageName"].ToString(), 
                            ContentType = sqlrd["ContentType"].ToString(),
                            ImageData = Convert.ToBase64String(imgBytes),
                        });
                        //Set the source with data:image/bmp
                        //img.Src = String.Format("data:image/Bmp;base64,{0}\"", imgString);   //img is the Image control ID
                    }
                }
            }
            catch (Exception e)
            {
                errmsg = "Error in loading Service Deploy List";
            }
            finally
            {
                sqlcon.Close();
            }

        }

        return newdt;
    }
}
