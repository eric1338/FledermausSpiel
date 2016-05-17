using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
    public interface ILogicalLevel
	{

		ILogicalRoom GetCurrentRoom();

	}
}
