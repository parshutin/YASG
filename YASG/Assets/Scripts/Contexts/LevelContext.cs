using Assets.Scripts.Commands.Level;
using Assets.Scripts.Commands.Level.GameField;
using Assets.Scripts.Core;
using Assets.Scripts.Enums;
using Assets.Scripts.Helpers;
using Assets.Scripts.Mediators.Game;
using Assets.Scripts.Mediators.Level;
using Assets.Scripts.Snake;
using Assets.Scripts.Views.Game;
using Assets.Scripts.Views.Level;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using strange.extensions.pool.api;
using strange.extensions.pool.impl;
using UnityEngine;

namespace Assets.Scripts.Contexts
{
    public class LevelContext : MVCSContext
    {
        public LevelContext(MonoBehaviour view) : base(view)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            injectionBinder.Bind<Field>().ToSingleton();
            injectionBinder.Bind<SnakeContainer>().ToSingleton();
            injectionBinder.Bind<FoodContainer>().ToSingleton();
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.WallPool);
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.SnakePartsPool);
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(GameElement.FoodPool);

            injectionBinder.Bind<GenerateFieldSignal>().ToSingleton();
            injectionBinder.Bind<CreateSnakeBodyPartSignal>().ToSingleton();
            injectionBinder.Bind<CreateFoodSignal>().ToSingleton();
            injectionBinder.Bind<RemoveFoodSignal>().ToSingleton();
            injectionBinder.Bind<CheckFieldSignal>().ToSingleton();

            mediationBinder.Bind<GameFieldView>().To<GameFieldMediator>();
            mediationBinder.Bind<SnakeBodyPartView>().To<SnakeBodyPartMediator>();

            commandBinder.Bind<GenerateFieldSignal>().To<GenerateFieldCommand>();
            commandBinder.Bind<CreateSnakeBodyPartSignal>().To<CreateSnakeBodyPartCommand>();
            commandBinder.Bind<CreateFoodSignal>().To<CreateFoodCommand>();
            commandBinder.Bind<RemoveFoodSignal>().To<RemoveFoodCommand>();
            commandBinder.Bind<CheckFieldSignal>().To<CheckFieldCommand>();
        }

        protected override void postBindings()
        {
            IPool<GameObject> wallPool = injectionBinder.GetInstance<IPool<GameObject>>(GameElement.WallPool);
            wallPool.instanceProvider = new ResourcesHelper("Green_Cube");
            wallPool.inflationType = PoolInflationType.INCREMENT;

            IPool<GameObject> snakePartsPool = injectionBinder.GetInstance<IPool<GameObject>>(GameElement.SnakePartsPool);
            snakePartsPool.instanceProvider = new ResourcesHelper("White_Cube");
            snakePartsPool.inflationType = PoolInflationType.INCREMENT;

            IPool<GameObject> foodPool = injectionBinder.GetInstance<IPool<GameObject>>(GameElement.FoodPool);
            foodPool.instanceProvider = new ResourcesHelper("Red_Cube");
            foodPool.inflationType = PoolInflationType.INCREMENT;
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }
    }
}
