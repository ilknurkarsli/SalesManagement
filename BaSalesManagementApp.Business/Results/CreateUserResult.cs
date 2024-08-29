using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Results
{
	public class CreateUserResult
	{
		public IdentityResult IdentityResult { get; set; }
		public string Password { get; set; }
	}

}
