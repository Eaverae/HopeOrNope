using System.IO;
using System.Threading.Tasks;

namespace GuidFramework.Services
{
	/// <summary>
	/// IFileService
	/// </summary>
	public interface IFileService
	{
		/// <summary>
		/// Gets the files from the assets folder
		/// </summary>
		/// <param name="directoryName">Name of the directory.</param>
		/// <returns></returns>
		Task<FileInfo[]> GetFilesFromAssetsAsync(string directoryName);

		/// <summary>
		/// Reads as bytes.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <returns></returns>
		Task<byte[]> ReadFromAssetsAsBytesAsync(string filename);

		/// <summary>
		/// Gets the files from internal storage.
		/// </summary>
		/// <param name="directoryName">Name of the directory.</param>
		/// <returns></returns>
		FileInfo[] GetFilesFromInternalStorage(string directoryName);

		/// <summary>
		/// Reads from internal storage asynchronous.
		/// </summary>
		/// <param name="fileName">The filename.</param>
		/// <param name="directoryName">The directory name</param>
		/// <returns>a string value</returns>
		Task<string> ReadFromInternalStorageAsync(string fileName, string directoryName = "persons");

		/// <summary>
		/// Opens the file from internal storage asynchronous.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="directoryName">Name of the directory.</param>
		/// <returns></returns>
		Task<byte[]> OpenFromInternalStorageAsync(string fileName, string directoryName = "persons");

		/// <summary>
		/// Saves the file to internal storage asynchronous;
		/// no other user or apps can access these files.
		/// </summary>
		/// <param name="fileContents">The file contents.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="directoryName">Name of the folder.</param>
		/// <returns>A string value with the filepath</returns>
		Task<string> SaveFileToInternalStorageAsync(string fileContents, string fileName, string directoryName = "persons");

		/// <summary>
		/// Saves the file to internal storage;
		/// no other user or apps can access these files.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="directoryName">Name of the folder.</param>
		/// <returns>A string value with the filepath</returns>
		string SaveFileToInternalStorage(byte[] file, string fileName, string directoryName = "persons");

		/// <summary>
		/// Clears the internal storage folder.
		/// </summary>
		/// <param name="directoryName">Name of the directory.</param>
		void ClearInternalStorageFolder(string directoryName = "persons");

		/// <summary>
		/// Deletes the file from internal storage.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		void DeleteFileFromInternalStorage(string fileName);

	}
}
