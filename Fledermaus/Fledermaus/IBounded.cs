using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	interface IBounded
	{

		IEnumerable<Line> GetLines();

	}
}
