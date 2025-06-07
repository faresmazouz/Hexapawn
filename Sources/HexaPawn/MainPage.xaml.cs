using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows.Input;
using ModelHexa;

namespace solution;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    private bool win = false;
    private bool isCanceled = true;
    private Player Player1 { get; set; } = new HumanPlayer("",TeamColor.Unknown);
    private Player Player2 { get; set; } = new HumanPlayer("", TeamColor.Unknown);
    private string _buttonText;
    public string textGiveUpButton
    {
        get => _buttonText;
        set
        {
            if (_buttonText != value)
            {
                _buttonText = value;
                OnPropertyChanged(nameof(textGiveUpButton));
            }
        }
    }
    private string _bandeau;
    public string Bandeau
    {
        get => _bandeau;
        set
        {
            if (_bandeau != value)
            {
                _bandeau = value;
                OnPropertyChanged(nameof(Bandeau));
            }
        }
    }
    private Rules Rules { get; set; } = new Rules();
    private ModelHexa.Cell selectedCell=new(10,1);
    private Board _plateau = new(3);
    private TeamColor courant = TeamColor.Player1;
    public Board Plateau
    {
        get => _plateau;
        set
        {
            if (_plateau != value)
            {
                _plateau = value;
                OnPropertyChanged(nameof(Plateau));
            }
        }
    }

    public ICommand CellTappedCommand { get; }

    public MainPage()
    {
        Bandeau = $"{TeamColor.Player1}";
        textGiveUpButton = "Abandonner";
        InitializeComponent();
        BindingContext = this;
        CellTappedCommand = new Command<ModelHexa.Cell>(OnCellTapped);
    }
    private void ChangeVisibility()
    {
        texteMode.IsVisible = !texteMode.IsVisible;
        cl.IsVisible = !cl.IsVisible;
        qxq.IsVisible = !qxq.IsVisible;
        cxc.IsVisible = !cxc.IsVisible;
        Scores.IsVisible = !Scores.IsVisible;
        uvu.IsVisible = !uvu.IsVisible;
        Bvu.IsVisible = !Bvu.IsVisible;
        BvB.IsVisible = !BvB.IsVisible;
        GiveUp.IsVisible = !GiveUp.IsVisible;
        Bndeau.IsVisible=!Bndeau.IsVisible;
    }

    public void HadToName(int nbNoms)
    {
        LePlateau.IsVisible = !LePlateau.IsVisible;
        nameEntry.IsVisible = !nameEntry.IsVisible;
        if (nbNoms == 2)
        {
            LabelName2.IsVisible = !LabelName2.IsVisible;
            EntryName2.IsVisible = !EntryName2.IsVisible;
            LabelName1.IsVisible = !LabelName1.IsVisible;
        }
        else 
        {
            LabelName.IsVisible = !LabelName.IsVisible;
            CheckBox1.IsVisible = !CheckBox1.IsVisible;
            LabelCheckBox.IsVisible = !LabelCheckBox.IsVisible;
        }
    }

    private async void OnCellTapped(ModelHexa.Cell cell)
    {
        if (playerCour() is not HumanPlayer) return; //Si le joueur courant n'est pas un humain, on ne fait rien
        if (win) return;
        Dictionary<ModelHexa.Cell, List<Move>> allMoves = Rules.allMoves(Plateau, courant);
        if (allMoves.Keys.Contains(cell)) //Si la cellule tapée est une pièce pouvant bouger, elle devient la pièce pour laquelle on fait le mouvement
        {
            selectedCell = cell;
            return;
        }
        Move tente = toMove(selectedCell, cell, courant); // Donne le nom du mouvement partant de selectedCell et arrivant à cell
        if (!allMoves.Keys.Contains(selectedCell)) return;
        if (allMoves[selectedCell].Contains(tente))
        {
            Plateau.MovePawn(Rules, playerCour(), selectedCell, tente, ref win);
            Switch(ref courant);
        }
        if (playerCour() is BOTPlayer&&!win && !isCanceled)
        {
            await Task.Delay(1000);
            if (isCanceled) return; // Si le jeu a été annulé, on ne continue pas
            bool choixFait = false;
            Dictionary<ModelHexa.Cell, List<Move>> AllMoves = Rules.allMoves(Plateau, courant);
            ModelHexa.Cell cellChoisie = playerCour().ChoosePawn(AllMoves);
            Move moveChoisi = playerCour().ChooseMove(AllMoves[cellChoisie], cellChoisie, ref choixFait);
            Plateau.MovePawn(Rules, playerCour(), cellChoisie, moveChoisi, ref win);
            Switch(ref courant);
        }
    }




    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    private void Switch(ref TeamColor a)
    {
        if (win)
        {
            textGiveUpButton = "Quitter";
            if (Player1 is BOTPlayer && Player2 is BOTPlayer) Bandeau = $"{courant} a gagné !";
            else Bandeau = $"{playerCour().Name} a gagné !";
            return;
        }
            if (a==TeamColor.Player1) a= TeamColor.Player2;
        else a = TeamColor.Player1;
        if (Player2 is BOTPlayer && Player1 is BOTPlayer) Bandeau = $"{a}";
        else Bandeau = $"{playerCour().Name}";

    }


    private async void OnClickedScores(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Scores());
    }
    private void OnClickedClassic(object sender, EventArgs e)
    {
        Plateau = new(3);
    }
    private void OnClicked4x4(object sender, EventArgs e)
    {
        Plateau = new(4);
    }
    private void OnClicked5x5(object sender, EventArgs e)
    {
        Plateau = new(5);
    }
    
    private void OnClicked1v1(object sender, EventArgs e)
    {
        isCanceled = false;
        HadToName(2);
        ChangeVisibility();
        courant = TeamColor.Player1;
        Bandeau = $"{playerCour().Name}";
    }
    private void OnClickedBv1(object sender, EventArgs e)
    {
        isCanceled = false;
        HadToName(1);
        ChangeVisibility();
        courant = TeamColor.Player1;
        Bandeau = $"{playerCour().Name}";
        
    }
    private async void OnClickedBvB(object sender, EventArgs e)
    {
        isCanceled = false;
        Player1=new BOTPlayer(TeamColor.Player1);
        Player2 = new BOTPlayer(TeamColor.Player2);
        ChangeVisibility();
        courant = TeamColor.Player1;
        Bandeau = $"{courant}"; 
        textGiveUpButton = "Arrêter";
        while (!win && !isCanceled)
        {
            bool choixFait = false;
            Dictionary<ModelHexa.Cell, List<Move>> allMoves = Rules.allMoves(Plateau, courant);
            ModelHexa.Cell cellChoisie = playerCour().ChoosePawn(allMoves);
            Move moveChoisi=playerCour().ChooseMove(allMoves[cellChoisie], cellChoisie, ref choixFait);
            Plateau.MovePawn(Rules, playerCour(), cellChoisie, moveChoisi, ref win);
            Switch(ref courant);
            await Task.Delay(1000);
        }
    }
    private void OnClickedGiveUp(object sender, EventArgs e)
    {
        if (nameEntry.IsVisible) return;
        isCanceled = true;
        ChangeVisibility();
        textGiveUpButton = "Abandonner";
        Plateau = new(3);
        win = false;
        Player1 = new HumanPlayer("", TeamColor.Unknown);
        Player2 = new HumanPlayer("",TeamColor.Unknown);
        Bandeau = $"{TeamColor.Player1}";
        textGiveUpButton = "Abandonner";
    }
    private async void OnClickedApparence(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ThemePage());
    }
    public static Move toMove(ModelHexa.Cell dep, ModelHexa.Cell fin, TeamColor t)
    {
        if (t == TeamColor.Player1)
        {
            if (dep.X + 1 == fin.X && dep.Y - 1 == fin.Y) return Move.eatLeft;
            if (dep.X + 1 == fin.X && dep.Y + 1 == fin.Y) return Move.eatRight;
            if (dep.X + 1 == fin.X && dep.Y == fin.Y) return Move.moveBy1;
            if (dep.X + 2 == fin.X && dep.Y == fin.Y) return Move.moveBy2;
            return Move.cantMove;
        }
        if (dep.X - 1 == fin.X && dep.Y - 1 == fin.Y) return Move.eatLeft;
        if (dep.X - 1 == fin.X && dep.Y + 1 == fin.Y) return Move.eatRight;
        if (dep.X - 1 == fin.X && dep.Y == fin.Y) return Move.moveBy1;
        if (dep.X - 2 == fin.X && dep.Y == fin.Y) return Move.moveBy2;
        return Move.cantMove;
    }
    private Player playerCour()
    {
        if (Player1.teamColor == courant) return Player1;
        return Player2;
    }

    private void OnClickedNameEntry(object sender, EventArgs e)
    {
        if (LabelName2.IsVisible)
        {
            if (string.IsNullOrWhiteSpace(EntryName2.Text) || string.IsNullOrWhiteSpace(EntryName1.Text) || EntryName2.Text == "Robot" || EntryName1.Text == "Robot" || EntryName2.Text == EntryName1.Text) return;
            Player1 = new HumanPlayer(EntryName1.Text, TeamColor.Player1);
            Player2 = new HumanPlayer(EntryName2.Text, TeamColor.Player2);
            HadToName(2);
            Bandeau=$"{playerCour().Name}";
        }
        else
        {
            if (string.IsNullOrWhiteSpace(EntryName1.Text) || EntryName1.Text == "Robot") return;
            if (CheckBox1.IsChecked)
            {
                Player1 = new HumanPlayer(EntryName1.Text, TeamColor.Player1);
                Player2 = new BOTPlayer(TeamColor.Player2);
            }
            else
            {
                Player2 = new HumanPlayer(EntryName1.Text, TeamColor.Player2);
                Player1 = new BOTPlayer(TeamColor.Player1);
            }
            HadToName(1);
            Bandeau = $"{playerCour().Name}";
            if (playerCour() is BOTPlayer)
            {
                bool choixFait = false;
                Dictionary<ModelHexa.Cell, List<Move>> AllMoves = Rules.allMoves(Plateau, courant);
                ModelHexa.Cell cellChoisie = playerCour().ChoosePawn(AllMoves);
                Move moveChoisi = playerCour().ChooseMove(AllMoves[cellChoisie], cellChoisie, ref choixFait);
                Plateau.MovePawn(Rules, playerCour(), cellChoisie, moveChoisi, ref win);
                Switch(ref courant);
            }
        }
    }
}