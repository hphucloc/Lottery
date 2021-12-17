using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GetData;
using HtmlAgilityPack;
using LotteryDAL;

namespace GetData
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

        private void btnGetData_Click(object sender, EventArgs e)
        {
            InsertKeno();
        }

        private LotteryEntities Db = LotteryDAL.LotteryConnection.Instance;
        private List<Number> ConvertListStringToListNumber3DMAX(List<string> input)
        {
            List<Number> val = new List<Number>();
            CultureInfo cul = new CultureInfo("vi-VN");
            for (int no = 0; no < input.Count; no++)
            {
                if (!Common.CheckDateExisted(Convert.ToDateTime(input[no], cul), (Int16)Enum_NumberType._3DMax))
                {
                    Console.WriteLine(input[no]);

                    Number aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = input[no + 4] + input[no + 8];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiNhat;
                    aNo.LotNumber = input[no + 14] + input[no + 18] + input[no + 22] + input[no + 26];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiNhi;
                    aNo.LotNumber = input[no + 32] + input[no + 36] + input[no + 40] + input[no + 44] + input[no + 48] + input[no + 52];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiBa;
                    aNo.LotNumber = input[no + 58] + input[no + 62] + input[no + 66] + input[no + 70] + input[no + 74] + input[no + 78] + input[no + 82] + input[no + 86];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);
                }
                no += 97;
            }

            return val;
        }
        private void Insert3DMax()
        {
            string value = null;
            string content = Common.Content2(webBrowser1.Document.Body.InnerHtml);

            var array = content.Split("\r\n        ".ToCharArray());
            var list = Common.RemoveEmptyElement(array);

            //Remove 4 first items in list
            for (byte i = 0; i <= 4; i++)
            {
                list.RemoveAt(0);
            }

            int c = 0;
            List<Number> ListNumber = ConvertListStringToListNumber3DMAX(list);
            foreach (Number lotNumber in ListNumber)
            {
                c++;
                value += lotNumber.LotNumber + " ";
                Db.Numbers.Add(lotNumber);
                Db.Entry(lotNumber).State = System.Data.Entity.EntityState.Added;
                Db.SaveChanges();

                if (c == 4)
                {
                    value += "\t(" + lotNumber.DatePublish.ToShortDateString() + ")\n\t";
                    c = 0;
                }
            }


            this.textBoxStatus.AppendText("+ " + DateTime.Now + ", Đã Lấy 3DMAX thành công các số:" + Environment.NewLine + (string.IsNullOrEmpty(value) ? "none" : value) + Environment.NewLine);
        }

        private List<Number> ConvertListStringToListNumber3DMAXPRO(List<string> input)
        {
            List<Number> val = new List<Number>();
            CultureInfo cul = new CultureInfo("vi-VN");
            for (int no = 0; no < input.Count; no++)
            {
                if (!Common.CheckDateExisted(Convert.ToDateTime(input[no], cul), (Int16)Enum_NumberType._3DMaxPro))
                {
                    Console.WriteLine(input[no]);

                    Number aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMaxPro;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = input[no + 4] + input[no + 8];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMaxPro;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiNhat;
                    aNo.LotNumber = input[no + 14] + input[no + 18] + input[no + 22] + input[no + 26];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMaxPro;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiNhi;
                    aNo.LotNumber = input[no + 32] + input[no + 36] + input[no + 40] + input[no + 44] + input[no + 48] + input[no + 52];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMaxPro;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiBa;
                    aNo.LotNumber = input[no + 58] + input[no + 62] + input[no + 66] + input[no + 70] + input[no + 74] + input[no + 78] + input[no + 82] + input[no + 86];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);
                }
                no += 97;
            }

            return val;
        }
        private void Insert3DMaxPro()
        {
            string value = null;
            string content = Common.Content2(webBrowser1.Document.Body.InnerHtml);

            var array = content.Split("\r\n        ".ToCharArray());
            var list = Common.RemoveEmptyElement(array);

            //Remove 4 first items in list
            for (byte i = 0; i <= 4; i++)
            {
                list.RemoveAt(0);
            }

            int c = 0;
            List<Number> ListNumber = ConvertListStringToListNumber3DMAXPRO(list);
            foreach (Number lotNumber in ListNumber)
            {
                c++;
                value += lotNumber.LotNumber + " ";
                Db.Numbers.Add(lotNumber);
                Db.Entry(lotNumber).State = System.Data.Entity.EntityState.Added;
                Db.SaveChanges();

                if (c == 4)
                {
                    value += "\t(" + lotNumber.DatePublish.ToShortDateString() + ")\n\t";
                    c = 0;
                }
            }


            this.textBoxStatus.AppendText("+ " + DateTime.Now + ", Đã Lấy 3DMAXPRO thành công các số:" + Environment.NewLine + (string.IsNullOrEmpty(value) ? "none" : value) + Environment.NewLine);
        }

        private List<Number> ConvertListStringToListNumberKENO(List<string> input)
        {
            List<Number> val = new List<Number>();
            CultureInfo cul = new CultureInfo("vi-VN");
            for (int no = 0; no < input.Count; no++)
            {
                if (input[no + 21].ToLower() == "Hòa".ToLower())
                {
                    input.Insert(no + 22, "");
                }
                if (input[no + 23].ToLower() == "Hòa".ToLower())
                {
                    input.Insert(no + 24, "");
                }
                if (!Common.CheckKyQuayExisted(Convert.ToInt32(input[no].Substring(11)), (Int16)Enum_NumberType._Keno))
                {
                    Number aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no].Substring(0, 10), cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._Keno;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    for (int j = 1; j <= 20; j++)
                    {
                        aNo.LotNumber += input[no + j];
                    }
                    aNo.DateCreated = DateTime.Now;
                    aNo.KyQuay = Convert.ToInt32(input[no].Substring(11));
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no].Substring(0, 10), cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._Keno;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiChanLe;
                    aNo.LotNumber = input[no + 21].ToLower() == "Hòa".ToLower() ? input[no + 21] : input[no + 21] + input[no + 22];
                    aNo.DateCreated = DateTime.Now;
                    aNo.KyQuay = Convert.ToInt32(input[no].Substring(11));
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no].Substring(0, 10), cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._Keno;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiLonNho;
                    aNo.LotNumber = input[no + 23].ToLower() == "Hòa".ToLower() ? input[no + 23] : input[no + 23] + input[no + 24];
                    aNo.DateCreated = DateTime.Now;
                    aNo.KyQuay = Convert.ToInt32(input[no].Substring(11));
                    val.Add(aNo);
                }
                no += 24;
            }

            return val;
        }
        private void InsertKeno()
        {
            string value = null;
            string content = Common.Content2(webBrowser1.Document.Body.InnerHtml);

            var array = content.Split("\r\n        ".ToCharArray());
            var list = Common.RemoveEmptyElement(array);

            //Remove 4 first items in list
            for (byte i = 0; i <= 5; i++)
            {
                list.RemoveAt(0);
            }

            int c = 0;
            List<Number> ListNumber = ConvertListStringToListNumberKENO(list);
            foreach (Number lotNumber in ListNumber)
            {
                c++;
                value += lotNumber.LotNumber + " ";
                Db.Numbers.Add(lotNumber);
                Db.Entry(lotNumber).State = System.Data.Entity.EntityState.Added;
                Db.SaveChanges();

                if (c == 3)
                {
                    value += "\t(" + lotNumber.DatePublish.ToShortDateString() + ", ky quay:" + lotNumber.KyQuay + ")\n\t";
                    c = 0;
                }
            }


            this.textBoxStatus.AppendText("+ " + DateTime.Now + ", Đã Lấy KENO thành công các số:" + Environment.NewLine + (string.IsNullOrEmpty(value) ? "none" : value) + Environment.NewLine);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.GetElementById("txtNumber01").InnerText = "123";
        }
    }
}
