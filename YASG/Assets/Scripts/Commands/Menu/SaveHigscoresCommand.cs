using Assets.Scripts.Helpers;
using strange.extensions.command.impl;

namespace Assets.Scripts.Commands.Menu
{
    public class SaveHigscoresCommand : Command
    {
        [Inject]
        public GDadaDbHelper DbHelper { get; set; }

        public override void Execute()
        {
            DbHelper.SaveHighscoresTable();
        }
    }
}