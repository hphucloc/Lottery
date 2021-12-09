<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>Using WebBrowser Control in ASP.NET</title>
</head>
<body>
<h2>Using WebBrowser Control in ASP.NET</h2>
    <form id="form1" runat="server">
    <div>
        <input id="Checkbox1" type="checkbox" runat="server" checked />&nbsp;Display form and WebBrowser control<p />
                    
        Login Microsoft live.com or hot mail: http://login.live.com<br />
        <table style="width: 426px">
            <tr>
                <td style="width: 3px; text-align: right;" align="right">
                    Email&nbsp;Address</td>
                <td style="width: 227px">
        <asp:TextBox ID="TextBox1" runat="server" Width="265px" TabIndex="9"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 3px; text-align: right;" align="right">
                    Password</td>
                <td style="width: 227px">
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" Width="265px" TabIndex="10"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 3px" align="right">
                </td>
                <td style="width: 227px">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" TabIndex="11" /></td>
            </tr>
        </table>

        <!--- get result of script vriables, cookies and input elements --->
        <%=result()%>
    </div>
    </form>
</body>
</html>
