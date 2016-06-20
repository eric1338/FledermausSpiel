using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	class Textures
	{

		public static Textures Instance = new Textures();

		private bool _texturesLoaded = false;

		public Texture PlayerTexture { get; set; }
		public Texture PlayerHitTexture { get; set; }
		public Texture FloorTexture { get; set; }
		public Texture ExitTexture { get; set; }
		public Texture ObstacleTexture1 { get; set; }
		public Texture ObstacleTexture2 { get; set; }
		public Texture ObstacleTexture3 { get; set; }
		public Texture ObstacleTexture4 { get; set; }
		public Texture TitleTexture { get; set; }

		public void LoadTextures()
		{
			if (_texturesLoaded) return;

			PlayerTexture = TextureLoader.FromBitmap(Resources.player);
			PlayerHitTexture = TextureLoader.FromBitmap(Resources.playerhit);
			FloorTexture = TextureLoader.FromBitmap(Resources.woodfloor);
			//ExitTexture = TextureLoader.FromBitmap();
			ObstacleTexture1 = TextureLoader.FromBitmap(Resources.obstacle1);
			ObstacleTexture2 = TextureLoader.FromBitmap(Resources.obstacle2);
			ObstacleTexture3 = TextureLoader.FromBitmap(Resources.obstacle3);
			ObstacleTexture4 = TextureLoader.FromBitmap(Resources.obstacle4);
			TitleTexture = TextureLoader.FromBitmap(Resources.title);

			_texturesLoaded = true;
		}

	}
}
