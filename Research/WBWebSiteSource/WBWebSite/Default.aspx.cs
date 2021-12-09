using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Threading;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{
    IEBrowser browser;

    // this is in the main thread
    protected void Button1_Click(object sender, EventArgs e)
    {
    //if (TextBox1.Text.Length == 0) return;
    //    if (TextBox2.Text.Length == 0) return;

        string userID = TextBox1.Text;
        string password = TextBox2.Text;
        AutoResetEvent resultEvent = new AutoResetEvent(false);
        string result = null;

        bool visible = this.Checkbox1.Checked;
        browser = new IEBrowser(visible, userID, password, resultEvent);

        // wait for the third thread getting result and setting result event
        EventWaitHandle.WaitAll(new AutoResetEvent[] { resultEvent });
        // the result is ready later than the result event setting somtimes 
        while (browser == null || browser.HtmlResult == null) Thread.Sleep(5);

        result = browser.HtmlResult;
        if (!visible) browser.Dispose();

        string path = Request.PhysicalApplicationPath;
        TextWriter tw = new StreamWriter(path + "result.html");
        tw.Write(result);
        tw.Close();

        Response.Output.WriteLine("<script>window.open ('result.html','mywindow','location=1,status=0,scrollbars=1,resizable=1,width=600,height=600');</script>");
    }

    // calling from Default.aspx
    public string result()
    {
        if (browser == null) return string.Empty;

        // navigation count
        string result = string.Format("<hr size=2 color='maroon'/><h4 align=center>Result Retrieved from WebBrowser Control</h4>Navigation count: {0}<p />", browser.NavigationCounter);
        // names, values and types of script variables
        result += browser.HtmlScriptTable;
        // names and values of cookies
        result += browser.HtmlCookieTable;
        // ids, names, values and values of HTML input elements
        result += browser.HtmlInputTable;

        return result;
    }
}
