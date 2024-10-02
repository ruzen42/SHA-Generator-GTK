using Gtk;
using System.Security.Cryptography;
using System.Text;

class ShaWindowApp : Window
{
    private Label name = new Label("SHA 256 Generator v1.1");
    private Entry inputEntry = new Entry { PlaceholderText = "Input text" };
    private Label Generated = new Label("Hash or text ");
    private Entry outputEntry = new Entry();
    private Button StartHashing = new Button("Start");

    private MenuBar menubar = new MenuBar();
    private MenuItem shaItem = new MenuItem("Select SHA");
    private Menu shamenu = new Menu();

    private MenuItem sha1Item = new MenuItem("SHA-1");
    private MenuItem sha256Item = new MenuItem("SHA-256");
    private MenuItem sha512Item = new MenuItem("SHA512");

    private static string? selectedAlgorithm = "SHA256";

    private ShaWindowApp() : base("SHA256 Generator")
    {
        DeleteEvent += DeleteWindowEvent;
        Box vertbox = new Box(Orientation.Vertical, 0);

        SetDefaultSize(650, 150);
        outputEntry.IsEditable = false;

        StartHashing.Clicked += StartHash;

        shamenu.Append(sha1Item);
        shamenu.Append(sha256Item);
        shamenu.Append(sha512Item);

        sha1Item.Activated += (sender, e) => { selectedAlgorithm = "SHA1"; };
        sha256Item.Activated += (sender, e) => { selectedAlgorithm = "SHA256"; };
        sha512Item.Activated += (sender, e) => { selectedAlgorithm = "SHA512"; };

        shaItem.Submenu = shamenu;

        menubar.Append(shaItem);

        vertbox.PackStart(name, false, false, 0);
        vertbox.PackStart(menubar, false, false, 0);
        vertbox.PackStart(inputEntry, false, false, 0);
        vertbox.PackStart(Generated, false, false, 0);
        vertbox.PackStart(outputEntry, false, false, 0);
        vertbox.PackStart(StartHashing, false, false, 0);

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
        outputEntry.Text = ComputeShaHash(inputEntry.Text);
    }

    private void DeleteWindowEvent(object o, EventArgs args)
    {
        Application.Quit();
        Console.WriteLine("App closed");
    }

    private static string ComputeShaHash(string rawData)
    {
        if (selectedAlgorithm == "SHA256")
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        else if (selectedAlgorithm == "SHA512")
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                var hash = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        else if (selectedAlgorithm == "SHA1")
        {
            using (SHA1 sha1Hash = SHA1.Create())
            {
                var hash = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        return "";
    }
}
