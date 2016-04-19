using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Mediators.Game;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Scripts.Commands.Level
{
    public class InitializeFieldSignal: Signal { }

    public class CreateSnakeBodyPartSignal : Signal<Cell, Transform> { }

    public class GenerateFieldSignal : Signal<Transform> { }

    public class CreateFoodSignal : Signal { }

    public class RemoveFoodSignal : Signal<int[]> { }

    public class StartLevelSignal : Signal { }

    public class StartGameSignal : Signal { }

    public class RestartGameSignal : Signal { }

    public class StopGameSignal : Signal { }

    public class AddSnakePartSignal : Signal<SnakeBodyPartMediator> { }

    public class CheckFieldSignal : Signal { }

    public class ChangeTimerIntervalSignal : Signal { }

    public class ChangeMovementDirectionSignal : Signal<MovementDirection> { }
}
