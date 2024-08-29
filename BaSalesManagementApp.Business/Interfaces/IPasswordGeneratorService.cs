using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Interfaces
{
	public interface IPasswordGeneratorService
	{
		Task<string> GenerateRandomPasswordAsync(int length = 8);

	}
}
