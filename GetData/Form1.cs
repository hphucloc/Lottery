using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebBrowserCSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Implement keypress on TextBox and display the URL in the browser        
        private void UrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenURLInBrowser(UrlTextBox.Text);
            }
        }

// GO button click event handler. 
private void GoButton_Click(object sender, EventArgs e)
{
    if (String.IsNullOrEmpty(UrlTextBox.Text) ||
        UrlTextBox.Text.Equals("about:blank"))
    {
        MessageBox.Show("Enter a valid URL.");
        UrlTextBox.Focus();
        return;
    }
    OpenURLInBrowser(UrlTextBox.Text);         
}

private void OpenURLInBrowser(string url)
{        
    if (!url.StartsWith("http://") &&
        !url.StartsWith("https://"))
    {
        url = "http://" + url;
    }
    try
    {
        webBrowser1.Navigate(new Uri(url));
    }
    catch (System.UriFormatException)
    {
        return;
    }
}

// Home button takes user home 
private void HomeButton_Click(object sender, EventArgs e)
{
    webBrowser1.GoHome();
}

// Go back
private void BackButton_Click(object sender, EventArgs e)
{
    if (webBrowser1.CanGoBack)
        webBrowser1.GoBack();
}

// Next 
private void NextButton_Click(object sender, EventArgs e)
{
    if (webBrowser1.CanGoForward)
        webBrowser1.GoForward();
}       

// Refresh
private void RefreshButton_Click(object sender, EventArgs e)
{
    webBrowser1.Refresh();
}

// Save button launches SaveAs dialog
private void SaveButton_Click(object sender, EventArgs e)
{
    webBrowser1.ShowSaveAsDialog();
}

// PrintPreview button launches PrintPreview dialog
private void PrintPreviewButton_Click(object sender, EventArgs e)
{
    webBrowser1.ShowPrintPreviewDialog();
}

// Properties button
private void PropertiesButton_Click(object sender, EventArgs e)
{
    webBrowser1.ShowPropertiesDialog();
}

// Show Print dialog
private void PrintButton_Click(object sender, EventArgs e)
{
    webBrowser1.ShowPrintDialog();
}
    }
}
