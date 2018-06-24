using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lrxw
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            importRadio.Tag = OperationMode.Import;
            alignRadio.Tag = OperationMode.Align;
            exportRadio.Tag = OperationMode.Export;

            locResFormat.SelectedIndex = 0; // XXX

            Application.Idle += Application_Idle;
        }

        private void ImpossibleCondition()
        {
            // XXX
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            // Update per Mode UI components.
            var key = Mode.ToString()[0]; // XXX
            foreach (Control c in Controls)
            {
                var tag = c.Tag as string;
                if (tag != null)
                {
                    c.Enabled = tag.IndexOf(key) >= 0;
                }
            }

            // Check whether all mandatory parameters are specified.
            TextBox[] mandatory = null;
            switch (Mode)
            {
                case OperationMode.Import:
                    mandatory = new[] { sourceLocRes, xliff, sourceLang, targetLang };
                    break;
                case OperationMode.Align:
                    mandatory = new[] { sourceLocRes, targetLocRes, xliff, sourceLang, targetLang };
                    break;
                case OperationMode.Export:
                    mandatory = new[] { targetLocRes, xliff };
                    break;
                default:
                    ImpossibleCondition();
                    break;
            }
            runButton.Enabled = mandatory?.All(t => !string.IsNullOrWhiteSpace(t.Text)) == true;
        }

        private enum OperationMode
        {
            Import,
            Align,
            Export,
        }

        private OperationMode Mode = OperationMode.Import;

        private void operation_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;
            if (radio?.Checked == true)
            {
                Mode = (OperationMode)radio.Tag;
            }
        }

        private void sourceButton_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case OperationMode.Import:
                case OperationMode.Align:
                    handleFileDialog(sourceOpenDialog, sourceLocRes);
                    break;
                default:
                    ImpossibleCondition();
                    break;
            }
        }

        private void targetButton_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case OperationMode.Align:
                    handleFileDialog(targetOpenDialog, targetLocRes);
                    break;
                case OperationMode.Export:
                    handleFileDialog(targetSaveDialog, targetLocRes);
                    break;
                default:
                    ImpossibleCondition();
                    break;
            }
        }

        private void xliffButton_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case OperationMode.Import:
                case OperationMode.Align:
                    handleFileDialog(xliffSaveDialog, xliff);
                    break;
                case OperationMode.Export:
                    handleFileDialog(xliffOpenDialog, xliff);
                    break;
                default:
                    ImpossibleCondition();
                    break;
            }
        }

        private void handleFileDialog(FileDialog dialog, TextBox text)
        {
            dialog.FileName = text.Text;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                text.Text = dialog.FileName;
            }
        }

        private async void runButton_Click(object sender, EventArgs e)
        {
            string[] args = null;
            switch (Mode)
            {
                case OperationMode.Import:
                    args = new[] { "-i", "-s", sourceLang.Text, "-t", targetLang.Text, sourceLocRes.Text, xliff.Text, };
                    break;
                case OperationMode.Align:
                    args = new[] { "-a", "-s", sourceLang.Text, "-t", targetLang.Text, sourceLocRes.Text, targetLocRes.Text, xliff.Text };
                    break;
                case OperationMode.Export:
                    var format = new[] { "auto", "0", "1" }[locResFormat.SelectedIndex];
                    args = new[] { "-e", "-f", format, xliff.Text, targetLocRes.Text };
                    break;
                default:
                    ImpossibleCondition();
                    break;
            }

            UseWaitCursor = true;
            Cursor.Current = Cursors.WaitCursor;
            await Task.Run(() => lrx.Program.Main(args));
            UseWaitCursor = false;
            Cursor.Current = Cursors.Default;
        }
    }
}
