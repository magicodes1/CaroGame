namespace CaroGame;

public class UserManager
{
    Panel leftLayout;
    Panel rightLayout;

    Button submit;

    UserUI userUI;

    Label player1Label = new Label();
    TextBox player1Text = new TextBox();
    RadioButton x1 = new RadioButton();
    RadioButton o1 = new RadioButton();

    Label player2Label = new Label();
    TextBox player2Text = new TextBox();
    RadioButton x2 = new RadioButton();
    RadioButton o2 = new RadioButton();

    string player1Name, player2Name, player1, player2;


    public string getPlayer1Name() => player1Name;
    public string getPlayer2Name() => player2Name;
    public string getPlayer1() => player1;
    public string getPlayer2() => player2;

    public UserManager(Panel leftLayout, Panel rightLayout, UserUI userUI, Button submit)
    {
        this.leftLayout = leftLayout;
        this.rightLayout = rightLayout;
        player1Name = player2Name = player1 = player2 = "";
        this.userUI = userUI;
        this.submit = submit;
    }

    public void createUserForm()
    {

        player1Label.Size = new System.Drawing.Size(190, 20);
        player1Label.Location = new Point(5, 10);
        player1Label.Font = new Font("Arial", 9.0f, FontStyle.Bold);
        player1Label.Text = "Player 1 Name";

        player1Text.Size = new System.Drawing.Size(190, 50);
        player1Text.Location = new Point(5, 30);

        x1.Text = "X";
        x1.Location = new Point(5, 70);
        x1.Font = new Font("Arial", 10.0f, FontStyle.Bold);
        x1.ForeColor = Color.Red;
        x1.Name = "rbt1x";

        x1.CheckedChanged += radioButton_CheckedChanged!;

        o1.Text = "O";
        o1.Location = new Point(5, 100);
        o1.Font = new Font("Arial", 10.0f, FontStyle.Bold);
        o1.ForeColor = Color.Green;
        o1.Name = "rbt1o";

        o1.CheckedChanged += radioButton_CheckedChanged!;
        //---------------------------------

        player2Label.Size = new System.Drawing.Size(190, 20);
        player2Label.Location = new Point(5, 10);
        player2Label.Font = new Font("Arial", 9.0f, FontStyle.Bold);
        player2Label.Text = "Player 2 Name";

        player2Text.Size = new System.Drawing.Size(190, 50);
        player2Text.Location = new Point(5, 30);

        x2.Text = "X";
        x2.Location = new Point(5, 70);
        x2.Font = new Font("Arial", 10.0f, FontStyle.Bold);
        x2.ForeColor = Color.Red;
        x2.CheckedChanged += radioButton_CheckedChanged!;
        x2.Name = "rbt2x";
        x2.Enabled = false;

        o2.Text = "O";
        o2.Location = new Point(5, 100);
        o2.Font = new Font("Arial", 10.0f, FontStyle.Bold);
        o2.ForeColor = Color.Green;
        o2.CheckedChanged += radioButton_CheckedChanged!;
        o2.Name = "rbtn2o";
        o2.Enabled = false;
        //------------------------------------



        leftLayout.Controls.Add(player1Label);
        leftLayout.Controls.Add(player1Text);
        leftLayout.Controls.Add(x1);
        leftLayout.Controls.Add(o1);


        rightLayout.Controls.Add(player2Label);
        rightLayout.Controls.Add(player2Text);
        rightLayout.Controls.Add(x2);
        rightLayout.Controls.Add(o2);
    }


    private void radioButton_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rb = (RadioButton)sender;

        switch (rb.Name)
        {
            case "rbt1x":
                o2.Enabled = true;
                o2.Checked = true;
                x2.Enabled = false;
                player1 = "x";
                player2 = "o";
                break;
            case "rbt1o":
                x2.Enabled = true;
                x2.Checked = true;
                o2.Enabled = false;
                player1 = "o";
                player2 = "x";
                break;
        }
    }

    private void inform()
    {
        string message = $"fill out the form";
        string caption = "Inform invalid form";
        MessageBoxButtons buttons = MessageBoxButtons.OK;
        DialogResult result;

        result = MessageBox.Show(message, caption, buttons);


    }

    public void reset()
    {
        leftLayout.Enabled=rightLayout.Enabled=submit.Enabled=true;
        
        player1Name = player2Name = player1 = player2 = "";
        player1Text.Text=player2Text.Text="";
        x1.Checked=x2.Checked=o1.Checked=o2.Checked=false;
        x2.Enabled=o2.Enabled=false;
    }

    public void getUserProfile()
    {
        if ((player1Text.Text == "" || player2Text.Text == "") || (!x1.Checked && !x2.Checked && !o1.Checked && !o2.Checked))
        {
            inform();
            return;
        }
        player1Name = player1Text.Text;
        player2Name = player2Text.Text;


        GameUI gameUI = new GameUI();
        gameUI.Show();
        leftLayout.Enabled=rightLayout.Enabled=submit.Enabled=false;
    }

}