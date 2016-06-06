using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using OpenTK;
using Model.GameObjectVisual;

namespace Fledermaus.Screens
{
    class LevelEditorGameScreen : Screen
    {
        MenuScreen menuScreen;

        bool showOverview = false;
        Level level;
        LevelVisual levelVisual;

        public LevelEditorGameScreen(Level level) : base()
        {
            this.level = level;
            menuScreen = new MenuScreen();
  /*          menuScreen.ContentWidth = this.ContentWidth;
            menuScreen.MaxHeight = .2f;
            menuScreen.Scale = Scale;
            menuScreen.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            menuScreen.ShowBorder = true;
            menuScreen.Center = new Vector2(this.Center.X, 1.0f - menuScreen.ContentHeight / 2);*/

            menuScreen.ShowBorder = true;
            menuScreen.Padding = .03f;
            menuScreen.BorderWidth = 0.01f;
            menuScreen.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            menuScreen.MaxHeight = .2f;
            menuScreen.ContentWidth = Scale * this.MaxWidth;
            menuScreen.Scale = Scale;
            menuScreen.Center = new Vector2(this.Center.X, 1.0f - menuScreen.ContentHeight / 2);  
                      
 //           menuScreen.menuButtons.Add(new ButtonText("Overview", delegate { ShowOverview = true; }));
 //           menuScreen.menuButtons.Add(new ButtonText("Overview", delegate { ShowOverview = true; }));
 //           menuScreen.menuButtons.Add(new ButtonText("Overview", delegate { ShowOverview = true; }));




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
        public LevelEditorGameScreen(LevelVisual level) : base() {
            levelVisual = level;
        }

        public bool ShowOverview
        {
            get
            {
                return showOverview;
            }

            set
            {
                if (value && !showBorder)
                    Scale *= 0.3f;
                else
                    Scale /= 0.3f;
                showOverview = value;
            }
        }

        public override void DoLogic()
        {
           
        }

        public override void Draw()
        {
            base.Draw();
//            menuScreen.Draw();
        }


        internal void Draw(Level level)
        {
            Draw();

            GameGraphicsLevelEditor.DrawLevel(level,Center, Scale);
        }
        internal void Draw(LevelVisual level)
        {
            levelVisual = level;
  //          Draw();
            foreach (var room in level.Rooms)
                room.Draw();
            //GameGraphicsLevelEditor.DrawLevel(level, Center, Scale);
        }

        public override void ProcessMouseMove(MouseMoveEventArgs e)
        {
            foreach (var room in levelVisual.Rooms) {
                if (e.Position.X > room.Data.Position.X - room.Data.RelativeBounds.First().X && e.Position.X < room.Data.Position.X + room.Data.RelativeBounds.First().X) {
                    if (e.Position.Y < room.Data.Position.Y - room.Data.RelativeBounds.First().Y && e.Position.Y > room.Data.Position.Y + room.Data.RelativeBounds.First().Y)
                    {
                        room.IsSelected = true;
                    }
                }
            }

        }

    }
}
