using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public int Lives { get; private set; }
    public int Balls { get; private set; }
    public bool IsAlive { get; private set; }

    public delegate void LivesChanged();
    public event LivesChanged OnLivesChanged;

    public Player(int startLives)
    {
        IsAlive = true;
        this.Lives = startLives;
    }

    public void BallLost()
    {
        Balls--;
        if (Balls == 0)
        {
            DecreaseLives();
        }
    }

    public void BallAdded(int balls)
    {
        Balls += balls;
    }

    public void AddLife(int count)
    {
        this.Lives += count;
        OnLivesChanged();
    }

    private void DecreaseLives()
    {
        Lives--;
        if (Lives == 0)
        {
            IsAlive = false;
        }
        OnLivesChanged();
    }

}
