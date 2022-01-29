namespace CaroGame;

public class GameManager
{
    private bool checkPlayer;
    private string[,] arr = new string[GameStorage.ROWS, GameStorage.COLUMNS];

    private FormClosingEventHandler formClosingEvent;

    private GameUI form;

    private Panel panel;

    private UserUI userUI;

    public void setCheckPlayer(bool checkPlayer)
    {
        this.checkPlayer = checkPlayer;
    }
    public bool getCheckPlayer() => checkPlayer;

    public GameManager(Panel panel, GameUI form, UserUI userUI, FormClosingEventHandler formClosingEvent)
    {
        this.panel = panel;
        this.form = form;
        this.userUI = userUI;
        this.formClosingEvent = formClosingEvent;
    }

    public void setArr(string[,] arr)
    {
        this.arr = arr;
    }
    public string[,] getArr() => arr;

    private void btn_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (btn.Text != "") return;

        string chessPieces = !checkPlayer ? "x" : "o";
        btn.Text = chessPieces;
        btn.ForeColor = !checkPlayer ? Color.Red : Color.Green;
        Font font = new Font("Arial", 18.0f,
                        FontStyle.Bold);
        btn.Font = font;

        var index = getIndex(btn.Tag.ToString()!);

        arr[index.Row, index.Column] = chessPieces;
        checkWinPlayer(index);

        checkPlayer = !checkPlayer;
    }


    private int checkMainDiagonal(ColumnRowIndex index, string chessPieces)
    {
        int row = index.Row;
        int column = index.Column;

        int countFirst = 0, countSecond = 0;

        while (row > 0 && column > 0)
        {
            string type = arr[--row, --column];

            if (type != null && type.CompareTo(chessPieces) == 0) countFirst++;
            else break;
        }

        row = index.Row;
        column = index.Column;

        while (row < GameStorage.ROWS - 1 && column < GameStorage.COLUMNS - 1)
        {
            string type = arr[++row, ++column];

            if (type != null && type.CompareTo(chessPieces) == 0) countSecond++;
            else break;
        }
        var result = countFirst + countSecond;

        return result;
    }

    private int checkSubDiagonal(ColumnRowIndex index, string chessPieces)
    {
        int row = index.Row;
        int column = index.Column;

        int countFirst = 0, countSecond = 0;
        while (row > 0 && column < GameStorage.COLUMNS)
        {
            string type = arr[--row, ++column];

            if (type != null && type.CompareTo(chessPieces) == 0) countFirst++;
            else break;
        }

        row = index.Row;
        column = index.Column;

        while (row < GameStorage.ROWS - 1 && column > 0)
        {
            string type = arr[++row, --column];

            if (type != null && type.CompareTo(chessPieces) == 0) countSecond++;
            else break;
        }
        var result = countFirst + countSecond;

        return result;
    }

    private int checkVerticalLine(ColumnRowIndex index, string chessPieces)
    {
        int row = index.Row;
        int column = index.Column;

        int countFirst = 0, countSecond = 0;

        while (row > 0 && (column > 0 && column < GameStorage.COLUMNS))
        {
            string type = arr[--row, column];

            if (type != null && type.CompareTo(chessPieces) == 0) countFirst++;
            else break;

        }

        row = index.Row;
        column = index.Column;

        while (row < GameStorage.ROWS - 1 && (column > 0 && column < GameStorage.COLUMNS))
        {
            string type = arr[++row, column];
            if (type != null && type.CompareTo(chessPieces) == 0) countSecond++;
            else break;
        }
        var result = countFirst + countSecond;

        return result;
    }

    private int checkHorizontal(ColumnRowIndex index, string chessPieces)
    {
        int row = index.Row;
        int column = index.Column;

        int countFirst = 0, countSecond = 0;
        while ((row > 0 && row < GameStorage.ROWS) && column > 0)
        {
            string type = arr[row, --column];

            if (type != null && type.CompareTo(chessPieces) == 0) countFirst++;
            else break;
        }

        row = index.Row;
        column = index.Column;

        while ((row > 0 && row < GameStorage.ROWS) && column < GameStorage.COLUMNS - 1)
        {
            string type = arr[row, ++column];
            if (type != null && type.CompareTo(chessPieces) == 0) countSecond++;
            else break;
        }
        var result = countFirst + countSecond;

        return result;
    }

    private void inform(string message, string caption, bool isClose)
    {
        MessageBoxButtons buttons = MessageBoxButtons.OK;
        DialogResult result;

        result = MessageBox.Show(message, caption, buttons);

        if (result == System.Windows.Forms.DialogResult.OK && isClose)
        {

            form.FormClosing -= formClosingEvent;
            form.Close();
            userUI.GetUserManager().reset();
        }
    }

    public ColumnRowIndex getIndex(string str)
    {
        string Str = str.Trim();

        string[] arr = Str.Split("-");

        return new ColumnRowIndex { Row = int.Parse(arr[0]), Column = int.Parse(arr[1]) };
    }

    public void checkWinPlayer(ColumnRowIndex index)
    {
        string chessPieces = arr[index.Row, index.Column];

        int totalMainDiagonal = checkMainDiagonal(index, chessPieces);
        int totalSubDiagonal = checkSubDiagonal(index, chessPieces);
        int totalVerticalLine = checkVerticalLine(index, chessPieces);
        int totalHorizontalLine = checkHorizontal(index, chessPieces);


        if ((totalMainDiagonal + 1) >= 5 || (totalSubDiagonal + 1) >= 5 || (totalVerticalLine + 1) == 5 || (totalHorizontalLine + 1) == 5)
        {
            inform($"{chessPieces} is winner", "Inform winner", true);
        }

    }
    public void chessBoard()
    {
        Button oldBtn = new Button { Width = 0, Location = new Point(0, 0) };

        for (int i = 0; i < GameStorage.ROWS; i++)
        {
            for (int j = 0; j < GameStorage.COLUMNS; j++)
            {
                Button currentBtn = new Button
                {
                    Width = GameStorage.SQUARE,
                    Height = GameStorage.SQUARE,
                    Name = $"btn{j}",
                    Location = new Point(oldBtn.Location.X + oldBtn.Width, oldBtn.Location.Y),
                    Tag = $"{i}-{j}"
                };
                currentBtn.Click += btn_Click!;

                panel.Controls.Add(currentBtn);
                oldBtn = currentBtn;
            }
            oldBtn.Location = new Point(0, oldBtn.Location.Y + GameStorage.SQUARE);
            oldBtn.Width = 0;
            oldBtn.Height = 0;
        }
    }

    public void randomUserGoFirst(string user1Name, string user1, string user2Name, string user2)
    {
        Random r = new Random();
        int index = r.Next(1, 100);

        //int index = 91;

        if (index % 2 == 0)
        {
            switch (user1)
            {
                case "x":
                    setCheckPlayer(false);
                    break;
                case "o":
                    setCheckPlayer(true);
                    break;
            }
            inform($"{user1Name} will go first", "Inform person who go first", false);
            return;
        }

        switch (user2)
        {
            case "x":
                setCheckPlayer(false);
                break;
            case "o":
                setCheckPlayer(true);
                break;
        }
        inform($"{user2Name} will go first", "Inform person who go first", false);
    }
}