using MongoDB.Bson;

namespace ToDo.Application.Extensions
{
	public static class ObjectIdExtensions
	{
		public static Guid ToGuid(this ObjectId objectId)
		{
			byte[] bytes = objectId.ToByteArray();
			byte[] guidBytes = new byte[16];
			Array.Copy(bytes, guidBytes, 16);
			return new Guid(guidBytes);
		}

		public static ObjectId ToObjectId(this Guid guid)
		{
			byte[] guidBytes = guid.ToByteArray();
			byte[] objectIdBytes = new byte[12];
			Array.Copy(guidBytes, objectIdBytes, 12);
			return new ObjectId(objectIdBytes);
		}
	}
}
