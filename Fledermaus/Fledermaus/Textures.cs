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
		public Texture FloorTexture { get; set; }
		public Texture ExitTexture { get; set; }
		public Texture ObstacleTexture { get; set; }

		public void LoadTextures()
		{
			if (!_texturesLoaded)
			{
				//PlayerTexture = TextureLoader.FromBitmap();
				FloorTexture = TextureLoader.FromBitmap(Resources.woodfloor);
				//ExitTexture = TextureLoader.FromBitmap();
				//ObstacleTexture = TextureLoader.FromBitmap();

				_texturesLoaded = true;
			}
		}

	}
}
