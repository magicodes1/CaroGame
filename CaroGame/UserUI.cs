namespace CaroGame;

public partial class UserUI : Form
{

    public static UserUI instance = null!;
    Panel leftLayout = new Panel();
    Panel rightLayout = new Panel();

    Button submit = new Button();

    UserManager userManager;

    public UserManager GetUserManager() => userManager;

    public UserUI()
    {
        InitializeComponent();
        leftLayout.Size = new System.Drawing.Size(200, 150);


        rightLayout.Size = new System.Drawing.Size(200, 150);
        rightLayout.Location = new Point(200, 0);

        submit.Location = new Point(120, 150);
        submit.Size = new System.Drawing.Size(150, 40);
        submit.BackColor = Color.Black;
        submit.Text = "Submit";
        submit.ForeColor = Color.White;
        submit.Font = new Font("Arial", 13.0f, FontStyle.Bold);

        submit.Click += submit_Click!;

        this.Controls.Add(leftLayout);
        this.Controls.Add(rightLayout);
        this.Controls.Add(submit);

        userManager = new UserManager(leftLayout, rightLayout, this, submit);
        userManager.createUserForm();
        instance = this;
    }


    private void submit_Click(object sender, EventArgs e)
    {
        userManager.getUserProfile();
    }

}