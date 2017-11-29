namespace demo.Helper
{
	public static class GlobalExtension
	{
		public static string FriendlyTitle(this string title)
		{
			if (!string.IsNullOrEmpty(title))
			{
				title = title.Trim().Replace(" ", "-").Replace("'", "-");
			}

			return title;
		}
	}
}
