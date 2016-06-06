
using Fledermaus.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
    static class LevelParser
    {
        public static Level parseModelLevel( Model.Level modLevel) {

            Level level = new Level();

            foreach (Model.GameObject.Room modRoom in modLevel.Rooms) { 
                Room room = new Room();
                room.Player = new Player(modRoom.Player.InitialPosition);

                foreach (var modLightRay in modRoom.LightRays) {
                    LightRay lr = new LightRay(modLightRay.Position, modLightRay.RayDirection);
                    room.AddLightRay(lr);
                }
                foreach (var modMirror in modRoom.Mirrors) {
                    Mirror m = new Mirror(modMirror.RailStart, modMirror.RailEnd, modMirror.InitAngle, modMirror.StartingRelativePosition);
                    room.AddMirror(m);
                }


            }
          //  var room = new Room();
         //   room.Player = new Fledermaus.GameObjects.Player(modLevel.Player.InitialPosition);
          //  room.Exit = new Fledermaus.GameObjects.RectangularGameObject();


            return level;
        }
        
    }
}
