using MySoftware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySoftware
{
    public class cMessageContent
    {
        public static string War_InterfaceCanNotEmpty
        {
            get
            {
                return "Interface can't empty.";
            }
        }
        public static string War_DeviceCanNotEmpty
        {
            get
            {
                return "Device can't empty.";
            }
        }
        public static string Que_ClearListImage
        {
            get
            {
                return "Do you want clear image list.";
            }
        }
        public static string Err_SLMPCannotConnect
        {
            get
            {
                return "SLMP can't connect.";
            }
        }
    }
    public class cMessageBox
    {
        public static void Notification(string Content)
        {
            MessageBox.Show(Content, cDialogCaptions.Infor, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Warning(string Content)
        {
            MessageBox.Show(Content, cDialogCaptions.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static DialogResult Question_YesNo(string Content)
        {
            return MessageBox.Show(Content, cDialogCaptions.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DialogResult Question_YesNoCancel(string Content)
        {
            return MessageBox.Show(Content, cDialogCaptions.Infor, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
        public static void Error(string Content)
        {
            MessageBox.Show(Content, cDialogCaptions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
