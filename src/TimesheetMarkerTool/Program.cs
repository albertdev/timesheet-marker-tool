namespace TimesheetMarkerTool;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        var forms = new Form1();
        forms.Show();

        string input = "P ";
        if (args.Length > 0)
        {
            input = String.Join(" ", args);
        }
        else if (InputBox("TimesheetMarkerTool - Input @@  @@", "Enter 'tag @ activity', or a shortcut", forms, ref input) == DialogResult.Cancel)
        {
            Application.Exit();
            return;
        }
        var tag = "Temporary";
        var activity = input;

        if (input.StartsWith("T "))
        {
            activity = input.Substring(2).Trim();
        }
        else if (input.StartsWith("P "))
        {
            tag = "Privé";
            activity = input.Substring(2).Trim();
        }
        else if (input.StartsWith("F "))
        {
            tag = "Fill";
            activity = input.Substring(2).Trim();
        }
        else if (input.Contains("@"))
        {
            int indexOf = input.IndexOf("@");
            tag = input.Substring(0, indexOf).Trim();
            activity = input.Substring(indexOf + 1).Trim();
        }
        activity = activity.Trim();

        forms.UpdateTitle(tag, activity);
        Application.Run(forms);
    }

    /// <summary>
    /// Copied (and modified) from https://www.csharp-examples.net/inputbox/
    /// </summary>
    public static DialogResult InputBox(string title, string promptText, IWin32Window parentWindow, ref string value)
    {
        Form form = new Form();
        Label label = new Label();
        TextBox textBox = new TextBox();
        Button buttonOk = new Button();
        Button buttonCancel = new Button();

        form.Text = title;
        label.Text = promptText;
        textBox.Text = value;

        buttonOk.Text = "OK";
        buttonCancel.Text = "Cancel";
        buttonOk.DialogResult = DialogResult.OK;
        buttonCancel.DialogResult = DialogResult.Cancel;

        label.SetBounds(9, 20, 372, 13);
        textBox.SetBounds(12, 36, 372, 20);
        buttonOk.SetBounds(228, 72, 75, 23);
        buttonCancel.SetBounds(309, 72, 75, 23);

        label.AutoSize = true;
        textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
        buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        form.ClientSize = new Size(396, 107);
        form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
        form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimizeBox = false;
        form.MaximizeBox = false;
        form.AcceptButton = buttonOk;
        form.CancelButton = buttonCancel;

        textBox.Select(value.Length, 0);

        DialogResult dialogResult = form.ShowDialog(parentWindow);
        value = textBox.Text;
        return dialogResult;
    }
}
