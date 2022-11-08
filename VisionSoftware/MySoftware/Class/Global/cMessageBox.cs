using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySoftware
{
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
