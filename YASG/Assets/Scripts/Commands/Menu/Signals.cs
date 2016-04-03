using System.Collections.Generic;
using Assets.Scripts.UserData;
using strange.extensions.signal.impl;

namespace Assets.Scripts.Commands.Menu
{
    public class OpenScoresSignal : Signal { }

    public class OpenMenuSignal : Signal { }

    public class HideScoresSignal : Signal { }

    public class InitScoresSignal : Signal<IEnumerable<UserScore>> { }

    public class ShowMenuSignal : Signal { }

    public class SaveHigscoresSignal : Signal { }

    public class ClearHigscoresSignal : Signal { }
}
