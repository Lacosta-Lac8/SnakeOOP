using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Interface
{
    public interface IGameObject
    {
        int x { get; }
        int y { get; }
        char symbol { get; }
    }
}
