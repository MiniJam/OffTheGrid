using System;

public class HexLocation : IEquatable<HexLocation>
{
	public HexLocation (int startingX, int startingZ)
	{
		this.x = startingX;
		this.z = startingZ;
	}

	public int x
	{
		get;
		set;
	}

	public int z
	{
		get;
		set;
	}

	public override int GetHashCode() {
		return ((short)this.x) << 16 + (short)this.z;
	}

	public override bool Equals(object obj) {
		return Equals(obj as HexLocation);
	}

	public bool Equals(HexLocation other) 
	{
		if (other == null) 
		{
			return false;
		}
		
		if (this.x == other.x && this.z == other.z) 
		{
			return true;
		}

		return false;
	}
}
