using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Interface
{
    public interface IGameRenderer
    {
        void ClearScreen();
        void RenderObject(int x, int y, char symbol);
        void RenderScore(int score, int height);
        void RenderGameOver(int score);
        void RenderWin(int score);
        void HideCursor();
    }
}
