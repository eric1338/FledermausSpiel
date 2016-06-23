using System;
using System.Collections.Generic;
using System.Text;

namespace Model.GameObject
{
    public class Room : GameObject
    {
        private int column;
        private int row;
        private int index;

        private Player player;
        private Exit exit;
        //private SolarPanel solarPanel;

        private List<LightRay> lightRays;
        private List<Mirror> mirrors;
       // private List<Enemy> enemies;
        private List<Obstacle> obstacles;


        public Player Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }

        public List<LightRay> LightRays
        {
            get
            {
                return lightRays;
            }

            set
            {
                lightRays = value;
            }
        }

  /*      public SolarPanel SolarPanel
        {
            get
            {
                return solarPanel;
            }

            set
            {
                solarPanel = value;
            }
        }*/

        public Exit Exit
        {
            get
            {
                return exit;
            }

            set
            {
                exit = value;
            }
        }

        public List<Mirror> Mirrors
        {
            get
            {
                return mirrors;
            }

            set
            {
                mirrors = value;
            }
        }
 /*       public List<Enemy> Enemies
        {
            get
            {
                return enemies;
            }

            set
            {
                enemies = value;
            }
        }*/

        public List<Obstacle> Obstacles
        {
            get
            {
                return obstacles;
            }

            set
            {
                obstacles = value;
            }
        }

        public int Row
        {
            get
            {
                return row;
            }

            set
            {
                row = value;
            }
        }

        public int Column
        {
            get
            {
                return column;
            }

            set
            {
                column = value;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }

        public Room()
        {
            
            //player = new Player();

            lightRays = new List<LightRay>();
            mirrors = new List<Mirror>();
            //enemies = new List<Enemy>();
            obstacles = new List<Obstacle>();
            exit = new Exit();
    }
    }
}
