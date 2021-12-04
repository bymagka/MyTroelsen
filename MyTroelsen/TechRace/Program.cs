using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Test
{
	public static void Main()
	{
        //Console.OutputEncoding = Encoding.UTF8;
        //Console.InputEncoding = Encoding.UTF8;
        string response = "something: ";
		string line;
		while ((line = Console.ReadLine()) != null && line != "")
		{
			response += line; // get the line
		}

		response = ConvertString(response);

		//here is your code
		Console.WriteLine(response);// return the string as a response
		Console.ReadLine();
	}

    private static string ConvertString(string response)
    {
		response = response.Replace("something: ", "");

		StringBuilder sb = new StringBuilder();
		

		char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

		int shift = int.Parse(response[response.Length-1].ToString());

		response = response.Substring(0, response.Length - 1);

		foreach(var item in response)
        {
			if (!alphabet.Contains(char.ToLower(item)))
            {
				sb.Append(item);
				continue;
			}
	

			int index = Array.IndexOf(alphabet, char.ToLower(item));

			if ((index - shift) >= alphabet.Length)
				index = (index - shift) - alphabet.Length - 1;
			else if ((index - shift) < 0)
				index = alphabet.Length - (shift - index);
			else
				index = index - shift;

			sb.Append(char.IsUpper(item) ? char.ToUpper(alphabet[index]) : alphabet[index]);
        }

		return sb.ToString();
	
		
    }
}