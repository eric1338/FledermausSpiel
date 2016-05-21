using Model.GameObject;
using System.Collections.Generic;


namespace Model
{
    public class Level
    {
        private Player player;
        private LightRay lightRay;
        private SolarPanel solarPanel;
        private Exit exit;


        private List<Mirror> mirrors;
        private List<Enemy> enemies;
        private List<Obstacle> obstacles;

        public Level() {
        }

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

        public LightRay LightRay
        {
            get
            {
                return lightRay;
            }

            set
            {
                lightRay = value;
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
    }
}
