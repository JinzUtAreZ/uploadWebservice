<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <%--<asp:FileUpload ID="FileUpload1" runat="server" />--%>
    <%--<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" />--%>
    <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnView" Text="View" runat="server" OnClick="View"  />--%>
    
     <br /><br />

     <div>
      <input type="file" ID="FileUpload1" runat="server"/>
      <img id="myImg" src="#" alt="your" height="200" width="200" />
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <input type="button" runat="server" onserverclick="Upload" value="Upload Files" />
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <input id="btnView" type="button" runat="server" onclick="return ViewData()" value="View Files" />
      <div id="imgContainer"></div>
      <br /><br />

      <div id="result"></div>
     
     </div>
    </form>

     

    <script type="text/javascript" >
        var tmpID = 0;

        window.addEventListener('load', function () {
            document
            .querySelector('input[type="file"]')
            .addEventListener('change', function () {
                if (this.files && this.files[0]) {
                    //var img = document.querySelector('img'); // $('img')[0]
                    var imgmulti = document.createElement('img');

                    //img.src = URL.createObjectURL(this.files[0]); // set src to blob url
                    imgmulti.src = URL.createObjectURL(this.files[0]); // set src to blob url
                    imgmulti.style.maxWidth = '200px';
                    imgmulti.style.maxHeight = '200px';
                    //img.onload = imageIsLoaded;

                    document.getElementById('imgContainer').appendChild(imgmulti);
                }
            });
        });

        function imageIsLoaded() {
            alert(this.src); // blob url
            // update width and height ...
        }

        function check_multifile_logo(file) {
            var extension = file.substr(file.lastIndexOf('.') + 1);
            if (extension === 'jpg' || extension === 'jpeg' || extension === 'gif' ||
                extension === 'png' || extension === 'bmp') 
            {
                return true;
            } else 
            {
                return false;
            }
        }

        function ViewData() {           
       
            postData('UploadService.asmx/ViewFile')
              .then((data) => {
                console.log(data); // JSON data parsed by `response.json()` call
                var dataset = data.d;
                alert(dataset[0].ContentType);
              });
        }

        async function postData(url = '', data = {}) {
          // Default options are marked with *
          const response = await fetch(url, {
            method: 'POST', // *GET, POST, PUT, DELETE, etc.
            mode: 'cors', // no-cors, *cors, same-origin
            cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
            credentials: 'same-origin', // include, *same-origin, omit
            headers: {
              'Content-Type': 'application/json'
              // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            redirect: 'follow', // manual, *follow, error
            referrerPolicy: 'no-referrer', // no-referrer, *client
            body: JSON.stringify(data) // body data type must match "Content-Type" header
          });
          return await response.json(); // parses JSON response into native JavaScript objects
        }

    </script>
</body>
</html>
