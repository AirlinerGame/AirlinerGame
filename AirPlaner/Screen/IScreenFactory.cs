using System;

namespace AirPlaner.Screen
{
    public interface IScreenFactory
    {
        GameScreen CreateScreen(Type screenType);
    }
}