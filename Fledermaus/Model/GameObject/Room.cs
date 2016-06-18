using System;
using System.Collections.Generic;
using System.Text;

namespace Model.GameObject
{
    public class Room : GameObject
    {

        private Player player;
        private int column;
        private int row;

        private SolarPanel solarPanel;

        private List<LightRay> lightRays;
        private List<Mirror> mirrors;
        private List<Enemy> enemies;
        private List<Obstacle> obstacles;
        private List<Exit> exits;

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

        public SolarPanel SolarPanel
        {
            get
            {
                return solarPanel;
            }

            set
            {
                solarPanel = value;
            }
        }

        public List<Exit> Exits
        {
            get
            {
                return exits;
            }

            set
            {
                exits = value;
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
        public List<Enemy> Enemies
        {
            get
            {
                return enemies;
            }

            set
            {
                enemies = value;
            }
        }

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

        public Room()
        {
            
            //player = new Player();

            lightRays = new List<LightRay>();
            mirrors = new List<Mirror>();
            enemies = new List<Enemy>();
            obstacles = new List<Obstacle>();
            exits = new List<Exit>();
    }
    }
}
