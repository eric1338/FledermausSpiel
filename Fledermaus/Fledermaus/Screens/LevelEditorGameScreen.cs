using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using OpenTK;

namespace Fledermaus.Screens
{
    class LevelEditorGameScreen : Screen
    {

        public LevelEditorGameScreen() : base()
        {

/*
            InputManager.Clear();
            InputManager.AddProlongedUserActionMapping(Key.Up, UserAction.MoveUp);
            InputManager.AddProlongedUserActionMapping(Key.Down, UserAction.MoveLeft);
            InputManager.AddProlongedUserActionMapping(Key.Left, UserAction.MoveDown);
            InputManager.AddProlongedUserActionMapping(Key.Right, UserAction.MoveRight);
            InputManager.AddProlongedUserActionMapping(Key.Enter, UserAction.RotateMirrorCW);
            InputManager.AddProlongedUserActionMapping(Key.Q, UserAction.RotateMirrorCCW);

            InputManager.AddSingleUserActionMapping(Key.Enter, UserAction.Confirm);
            InputManager.AddSingleUserActionMapping(Key.Escape, UserAction.Cancel);
       */
    }

        public override void DoLogic()
        {
           
        }

        public override void Draw()
        {
            base.Draw();
        }

        internal void Draw(Level level)
        {
            Draw();
            GameGraphicsLevelEditor.DrawLevel(level,Center, Scale);
        }
    }
}
