using System.ComponentModel;
using AirPlaner.Screen;

namespace AirPlaner.Game.Screen.State
{
    public interface IAsyncLoadable
    {
        void AsyncContentLoad(BackgroundWorker worker);
    }
}
