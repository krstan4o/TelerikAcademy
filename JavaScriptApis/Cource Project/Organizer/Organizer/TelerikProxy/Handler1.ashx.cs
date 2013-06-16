using System.Text;
using System.Web;
using System.Net;

public class Proxy : IHttpHandler {

    public void ProcessRequest(HttpContext context) {
        context.Response.ContentType = "application/json";
        using (WebClient client = new WebClient())  
        {
            client.Encoding = Encoding.UTF8;
            context.Response.BinaryWrite(client.DownloadData(context.Request.QueryString["url"]));
        }
    }

    public bool IsReusable { get { return true; } }

}