using System.Text.Json;
using System.Xml.Serialization;

namespace GameServer.Helpers
{
	/// <summary>
	/// An static class which Serializes/Deserializes XML files.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class Serializer<T> where T : new()
	{
		#region Methods

		/// <summary>
		/// Serializes an object in an xml file.
		/// </summary>
		/// <param name="path">The path to the destination file.</param>
		/// <param name="obj">The object to be serialized.</param>
		public static void SaveXml(string path, T obj)
		{
			try
			{
				using var sw = new StreamWriter(path, false);
				var xs = new XmlSerializer(typeof(T));

				xs.Serialize(sw, obj);
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Deserializes an xml file in an object.
		/// </summary>
		/// <param name="path">The path to the destination file.</param>
		/// <returns>The object deserialized from the file</returns>
		public static T LoadXml(string path)
		{
			try
			{
				if (File.Exists(path))
				{
					using var sr = new StreamReader(path);
					var xs = new XmlSerializer(typeof(T));

					var result = (T?)xs.Deserialize(sr);
					if (result != null)
						return result;
				}
			}
			catch (Exception)
			{
				throw;
			}

			SaveXml(path, new T());

			return new T();
		}

		/*public static T LoadXml(Uri url)
		{
			try
			{
				using(var wc = new WebClient())
				using(var sr = new StreamReader(wc.OpenRead(url)))
				{
					var xs = new XmlSerializer(typeof(T));

					return (T)xs.Deserialize(sr);
				}
			}
			catch(Exception)
			{
				throw;
			}
		}*/

		public static T LoadJson(string path)
		{
			try
			{
				if (File.Exists(path))
				{
					using var sr = new StreamReader(path);
					JsonSerializerOptions options = new()
					{
						AllowTrailingCommas = true,
						IgnoreReadOnlyFields = true,
					};

					var result = JsonSerializer.Deserialize<T>(sr.BaseStream, options);
					if (result != null)
						return result;
				}
			}
			catch (Exception)
			{
				throw;
			}

			SaveJson(path, new T());

			return new T();
		}

		public static void SaveJson(string path, T obj, bool writeIndented = false)
		{
			try
			{
				using var sw = new StreamWriter(path, false);
				JsonSerializerOptions options = new()
				{
					WriteIndented = writeIndented,
					AllowTrailingCommas = true,
					IgnoreReadOnlyFields = true,
				};

				JsonSerializer.Serialize<T>(sw.BaseStream, obj, options);
			}
			catch (Exception)
			{
				throw;
			}
		}

		#endregion
	}
}
