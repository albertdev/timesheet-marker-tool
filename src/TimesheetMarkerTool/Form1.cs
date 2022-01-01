namespace TimesheetMarkerTool;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        this.Text = "TimesheetMarkerTool - Input @@  @@";
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    internal void UpdateTitle(string tag, string activity)
    {
        this.Text = $"TimesheetMarkerTool - {tag} @@ {activity} @@";
    }
}
