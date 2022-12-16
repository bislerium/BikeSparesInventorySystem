using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSparesInventorySystem.Shared
{
	public partial class NavMenu
	{
		private bool _open = true;

		void ToggleDrawer()
		{
			_open = !_open;
		}
	
	}
}
