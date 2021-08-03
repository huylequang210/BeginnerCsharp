using System;
using System.Reflection;
using System.Collections.Generic;

namespace Factory
{
	public class Cat
	{
		public string name { get; set; }
		public int age { get; set; }
		public bool gender { get; set; }
		public string dawdwad { get; set; }
	}

	public class Factory<TObject> where TObject : new()
	{
		public List<TObject> Create(int num)
		{
			List<TObject> lisTObject = new List<TObject>();
			for (int i = 0; i < num; i++)
			{
				TObject tObject = new TObject();
				var TObjectType = typeof(TObject);
				PropertyInfo[] properties = TObjectType.GetProperties();
				foreach (PropertyInfo property in properties)
				{
					TObjectType.GetProperty(property.Name).SetValue(tObject, Generate<dynamic>(property.Name));
				}
				lisTObject.Add(tObject);
			}
			return lisTObject;
		}

		public T Generate<T>(string property)
		{
			T value;
			switch (property)
			{
				case "name":
					value = ConvertValue<T, string>(NameGenerate());
					break;
				case "age":
					value = ConvertValue<T, int>(AgeGenerate());
					break;
				default:
					value = ConvertValue<T, T>(default);
					break;
			}
			return value;
		}
		
		// from stackoverflow
		private static string NameGenerate()
		{
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var stringChars = new char[8];
			var random = new Random();
			for (int i = 0; i < stringChars.Length; i++)
			{
				stringChars[i] = chars[random.Next(chars.Length)];
			}
			var finalString = new string(stringChars);
			return finalString;
		}
		
		private static int AgeGenerate()
		{
			Random rnd = new Random();
			return rnd.Next(1, 13);
		}

		private static T ConvertValue<T,U>(U value)
		{
			return (T)Convert.ChangeType(value, typeof(T));
		}
	}

	public class Program
	{
		public static void Main()
		{
			var Cat = new Factory<Cat>();
			var listCats = Cat.Create(10);


			foreach (var cat in listCats)
			{
				Console.WriteLine(cat.gender);
			}
		}
	}
}
