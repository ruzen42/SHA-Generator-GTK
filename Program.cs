using Gtk;
using System.Security.Cryptography;
using System.Text;

class ShaWindowApp : Window
{
    private Label name = new Label("SHA 256 Generator v1.0");
    private Entry inputEntry = new Entry { PlaceholderText = "Input text" };
    private Label Generated = new Label("Hash or text ");
    private Entry outputEntry = new Entry();
    private Button StartHashing = new Button("Start");

    private ShaWindowApp() : base("SHA256 Generator")
    {
        DeleteEvent += DeleteWindowEvent;
        Box vertbox = new Box(Orientation.Vertical, 5);

        SetDefaultSize(650, 150);
        outputEntry.IsEditable = false;

        StartHashing.Clicked += StartHash;

        vertbox.PackStart(name, false, false, 0);

        vertbox.PackStart(inputEntry, false, false, 0);
        vertbox.PackStart(Generated, false, false, 0);
        vertbox.PackStart(outputEntry, false, false, 0);


        Add(vertbox);
        ShowAll();
    }

    private static void Main()
    {
        Application.Init();
        new ShaWindowApp();
        Application.Run();
    }

    private void StartHash(object? o, EventArgs? args)
    {
        outputEntry.Text = ComputeSha256Hash(inputEntry.Text);
    }


    private void DeleteWindowEvent(object o, EventArgs args)
    {
        Application.Quit();
        Console.WriteLine("App closed");
    }

    private static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
