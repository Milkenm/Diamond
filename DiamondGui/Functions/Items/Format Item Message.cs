namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static string FormatItemMessage(string itemName, double itemPrice, int amount)
		{
			return $"__**{amount}**x **{itemName}**__ for **{itemPrice * amount}**";
		}
	}
}