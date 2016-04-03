using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Commands.Menu;
using Assets.Scripts.Helpers;
using Assets.Scripts.Mediators.Menu;
using Assets.Scripts.Views.Menu;
using strange.examples.signals;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using UnityEngine;

namespace Assets.Scripts.Contexts
{
    public class MenuContext : MVCSContext
    {
        public MenuContext(MonoBehaviour view) : base(view)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();
            injectionBinder.Bind<GDadaDbHelper>().ToSingleton();
            injectionBinder.Bind<ShowMenuSignal>().ToSingleton();
            injectionBinder.Bind<InitScoresSignal>().ToSingleton();
            injectionBinder.Bind<ClearHigscoresSignal>().ToSingleton();
            injectionBinder.Bind<OpenMenuSignal>().ToSingleton();
            injectionBinder.Bind<OpenScoresSignal>().ToSingleton();
            injectionBinder.Bind<SaveHigscoresSignal>().ToSingleton();

            commandBinder.Bind<ClearHigscoresSignal>().To<ClearScoresCommand>();
            commandBinder.Bind<OpenMenuSignal>().To<OpenMenuCommand>();
            commandBinder.Bind<OpenScoresSignal>().To<OpenScoresCommand>();
            commandBinder.Bind<SaveHigscoresSignal>().To<SaveHigscoresCommand>();

            mediationBinder.Bind<MenuView>().To<MenuMediator>();
            mediationBinder.Bind<ScoresView>().To<ScoresMediator>();
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }
    }
}
