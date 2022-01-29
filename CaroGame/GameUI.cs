namespace CaroGame;

public partial class GameUI : Form
{

    private Panel panel = new Panel();
    private GameManager gameManager;

    private string player1Name = "", player2Name = "", player1 = "", player2 = "";

    public GameUI()
    {
        InitializeComponent();



        panel.Size = new System.Drawing.Size(GameStorage.CHESS_BOARD_WIDTH, GameStorage.CHESS_BOARD_HEIGHT);
        this.Controls.Add(panel);
        gameManager = new GameManager(panel, this, UserUI.instance,myWindow_FormClosing!);
        gameManager.chessBoard();

        player1Name = UserUI.instance.GetUserManager().getPlayer1Name();
        player2Name = UserUI.instance.GetUserManager().getPlayer2Name();
        player1 = UserUI.instance.GetUserManager().getPlayer1();
        player2 = UserUI.instance.GetUserManager().getPlayer2();
        gameManager.randomUserGoFirst(player1Name, player1, player2Name, player2);

        this.FormClosing+=myWindow_FormClosing!;
    }

    private void myWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
    }
}
