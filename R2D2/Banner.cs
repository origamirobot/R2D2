namespace R2D2
{
	public static class Banner
	{

		public static void Show()
		{
			var lines = File.ReadAllLines("Banner.txt");
			foreach(var line in lines) Console.WriteLine(line);
		}

	}
}
