using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
		/// Saves the file to internal storage;
		/// no other user or apps can access these files.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="directoryName">Name of the folder.</param>
		/// <returns>A string value with the filepath</returns>
		string SaveFileToInternalStorage(byte[] file, string fileName, string directoryName = "scans");

		/// <summary>
		/// Clears the internal storage folder.
		/// </summary>
		/// <param name="directoryName">Name of the directory.</param>
		void ClearInternalStorageFolder(string directoryName = "scans");

	}
}
