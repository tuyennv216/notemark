namespace notemark.Masks;

public class MaskDictionary
{
	public static Dictionary<KeyType, ItemType> FromArray<KeyType, ItemType>(ItemType[] arr, Func<ItemType, KeyType> getKey) where KeyType : notnull
	{
		var output = new Dictionary<KeyType, ItemType>();
		Array.ForEach(arr, item =>
		{
			output[getKey(item)] = item;
		});
		return output;
	}
}
