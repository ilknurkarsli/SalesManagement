using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaSalesManagementApp.Business.Services
{
	public class PasswordGeneratorService : IPasswordGeneratorService
	{
		private static readonly Random random = new Random();
		public Task<string> GenerateRandomPasswordAsync(int length = 8)
		{
			return Task.Run(() =>
			{
				const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
				const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
				const string digits = "0123456789";
				const string specialChars = "!@#$%^&*()_+[]{}";

				var allChars = upperCase + lowerCase + digits + specialChars;
				var passwordChars = new char[length];

				passwordChars[0] = upperCase[random.Next(upperCase.Length)];
				passwordChars[1] = lowerCase[random.Next(lowerCase.Length)];
				passwordChars[2] = digits[random.Next(digits.Length)];
				passwordChars[3] = specialChars[random.Next(specialChars.Length)];

				for (int i = 4; i < length; i++)
				{
					passwordChars[i] = allChars[random.Next(allChars.Length)];
				}

				return new string(passwordChars.OrderBy(x => random.Next()).ToArray());
			});
		}
	}
}
