using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class CS : System.Web.UI.Page
{
    protected void Upload(object sender, EventArgs e)
    {
        //Get the name of the File.
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

        //Get the content type of the File.
        string contentType = FileUpload1.PostedFile.ContentType;

        //Read the file data into Byte Array.
        BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream);
        byte[] bytes = br.ReadBytes((int)FileUpload1.PostedFile.InputStream.Length);

        //Call the Web Service and pass the File data for upload.
        uploader.UploadService service = new uploader.UploadService();
        
        service.UploadFile(fileName, contentType, bytes);

    }

    protected void View(object sender, EventArgs e)
    {
        uploader.UploadService service = new uploader.UploadService();
        service.ViewFile();
    }

//    USE [EQM]
//GO
///****** Object:  StoredProcedure [dbo].[spSave_ImageData]    Script Date: 02/24/2020 18:03:31 ******/
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//-- =============================================
//-- Author:		<Author,,Name>
//-- Create date: <Create Date,,>
//-- Description:	<Description,,>
//-- =============================================
//ALTER PROCEDURE [dbo].[spSave_ImageData]
//    @inFileName as varchar(100) = null,
//    @inType as varchar(100) = null,
//    @inData as varbinary(max) = null
//AS
//BEGIN

//    INSERT INTO t_UploadData([ImageName],[ContentType],[Data])
//    VALUES(@inFileName,@inType,@inData)
   
//END


//    USE [EQM]
//GO
///****** Object:  StoredProcedure [dbo].[spLoad_ImageData]    Script Date: 02/24/2020 18:04:10 ******/
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//-- =============================================
//-- Author:		<Author,,Name>
//-- Create date: <Create Date,,>
//-- Description:	<Description,,>
//-- =============================================
//ALTER PROCEDURE [dbo].[spLoad_ImageData] 
	
//AS
//BEGIN
//    SELECT Data,ImageName,ContentType
//            FROM t_UploadData Where RowID = 6
	
//    ------- convert base64 data ung nasa baba -------		
//    --Declare @Data as varbinary(max),@Name as varchar(50),@ContentType as varchar(50),@encoded varchar(max)

//    --create table #TempoTable(Base64Data varchar(max),Name varchar(50),Content varchar(50))
 
//    --DECLARE Data_Cursor CURSOR FOR
//    --		SELECT Data,ImageName,ContentType
//    --		FROM t_UploadData Where RowID = 6
//    --OPEN Data_Cursor
//    --FETCH NEXT FROM Data_Cursor
//    --Into @Data,@Name,@ContentType

//    --	WHILE @@FETCH_STATUS = 0
//    --	BEGIN
//    --	  set @encoded = cast('' as xml).value('xs:base64Binary(sql:variable("@Data"))', 'varchar(max)')
		  
//    --	  Insert Into #TempoTable 	
//    --		 values(@encoded,@Name,@ContentType)
			 
//    --	  FETCH NEXT FROM Data_Cursor
//    --	  Into @Data,@Name,@ContentType
		
//    --	END
//    --	Select * From #TempoTable
		
//    --CLOSE Data_Cursor
//    --DEALLOCATE Data_Cursor

//    --drop table #TempoTable
//END

}
