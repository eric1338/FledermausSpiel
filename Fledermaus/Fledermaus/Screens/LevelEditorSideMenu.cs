using Model;
using Model.GameObject;
using OpenTK;
using System.Collections.Generic;

namespace Fledermaus.Screens
{
    internal class LevelEditorSideMenu : MenuScreen
    {
        private LevelEditorScreen owner;

        public LevelEditorSideMenu(LevelEditorScreen owner) : base()
        {
            this.owner = owner;
            owner.setInputManager(this._inputManager);
            

            menuButtons.Add(
                new ButtonTexture(Resources.Player,
                delegate () {
                    addPlayer();
                },
                true)
            );
            menuButtons.Add(
    new ButtonTexture(Resources.Player,
    delegate () {
        addPlayer();
    },
    false)
);

        }
        private void addPlayer()
        {
            owner.Level.Player = new Player()
            {
                RelativeBounds = new List<Vector2>(){
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(0.1f)),
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(0.1f)),
                        },
            };
        }
    }
}