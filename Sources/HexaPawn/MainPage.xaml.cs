<<<<<<< HEAD
﻿using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows.Input;
using ModelHexa;

namespace solution;
=======
namespace Hexapawn;
>>>>>>> origin/data

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    private bool win = false;
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
    private TeamColor courant = TeamColor.Unknown;
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
    }




    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    private void Switch(ref TeamColor a)
    {
        if (win)
        {
            textGiveUpButton = "Quitter";
            Bandeau = $"{playerCour().Name} a gagn� !";
            return;
        }
            if (a==TeamColor.Player1) a= TeamColor.Player2;
        else a = TeamColor.Player1;
        Bandeau = $"{a}";
        
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
        ChangeVisibility();
        Player1 = new HumanPlayer("P1",TeamColor.Player1);
        Player2 = new HumanPlayer("P2", TeamColor.Player2);
        courant = TeamColor.Player1;
    }
    private void OnClickedBv1(object sender, EventArgs e)
    {
        ChangeVisibility();
    }
    private void OnClickedBvB(object sender, EventArgs e)
    {
        ChangeVisibility();
        Player1=new BOTPlayer(TeamColor.Player1);
        Player2 = new BOTPlayer(TeamColor.Player2);
        TeamColor winner = TeamColor.Unknown;
        while (!win)
        {
            Player1.PlayTurn(Rules.allMoves(Plateau, TeamColor.Player1), Plateau, Rules, Player2, ref winner);
        }
    }
    private void OnClickedGiveUp(object sender, EventArgs e)
    {
        ChangeVisibility();
        textGiveUpButton = "Abandonner";
        Plateau = new(3);
        win = false;
        Player1 = new HumanPlayer("", TeamColor.Unknown);
        Player2 = new HumanPlayer("", TeamColor.Unknown);
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
    private async void OnClickedRules(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RulesPage());
    }
}