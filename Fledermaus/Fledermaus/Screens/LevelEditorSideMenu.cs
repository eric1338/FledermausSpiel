using Model;
using Model.GameObject;
using OpenTK;
using System.Collections.Generic;

namespace Fledermaus.Screens
{
    internal class LevelEditorSideMenu : MenuScreen
    {

        public LevelEditorSideMenu( LevelEditorScreen owner) : base()
        {
            menuButtons.Add(
                new ButtonTexture(Resources.Player, 
                delegate () {
                    owner.Level.Player = new Player() {
                        Position = new Vector2(0f,0f),
                        RelativeBounds = new List<Vector2>(){
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(0.1f)),
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(0.1f)),
                        },
            }; }, true));
            menuButtons.Add(
                new ButtonTexture(Resources.Player,
                delegate () {
                    owner.Level.Player = new Player()
                    {
                        Position = new Vector2(0f, 0f),
                        RelativeBounds = new List<Vector2>(){
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(0.1f)),
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(0.1f)),
                        },
                    };
                }));
            menuButtons.Add(
                new ButtonTexture(Resources.Player,
                delegate () {
                    owner.Level.Player = new Player()
                    {
                        Position = new Vector2(0f, 0f),
                        RelativeBounds = new List<Vector2>(){
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(0.1f)),
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(0.1f)),
                        },
                    };
                }));
            menuButtons.Add(
                new ButtonTexture(Resources.Player,
                delegate () {
                    owner.Level.Player = new Player()
                    {
                        Position = new Vector2(0f, 0f),
                        RelativeBounds = new List<Vector2>(){
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(0.1f)),
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(0.1f)),
                        },
                    };
                }));
            menuButtons.Add(
                new ButtonTexture(Resources.Player,
                delegate () {
                    owner.Level.Player = new Player()
                    {
                        Position = new Vector2(0f, 0f),
                        RelativeBounds = new List<Vector2>(){
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(-0.1f)),
                            new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(0.1f)),
                            new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(0.1f)),
                        },
                    };
                }));

        }
    }
}