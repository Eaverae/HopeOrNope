using GuidFramework.Services;
using GuidFramework.Extensions;
using GuidFramework.iOS.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace GuidFramework.iOS.Services
{
	/// <summary>
	/// FileService
	/// </summary>
	/// <seealso cref="Rimek.Framework.App.Interfaces.IFileService" />
	public class FileService : IFileService
	{
		/// <summary>
		/// Clears the internal storage folder.
		/// </summary>
		/// <param name="directoryName">Name of the directory.</param>
		/// <exception cref="ArgumentNullException">directoryName</exception>
		public void ClearInternalStorageFolder(string directoryName = "scans")
		{
			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), directoryName);

			if (Directory.Exists(folder))
				Directory.Delete(folder, true);
		}

		/// <summary>
		/// Gets the files asynchronous.
		/// </summary>
		/// <param name="directoryName">Name of the directory.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">directoryName</exception>
		public Task<FileInfo[]> GetFilesFromAssetsAsync(string directoryName)
		{
			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			return Task.Run(() =>
			{
				FileInfo[] fileInfos = null;

				string[] files = Directory.GetFiles(directoryName);

				if (files != null && files.Any())
					fileInfos = files.Select(item => new FileInfo(item)).OrderBy(item => item.Name).ToArray();

				return fileInfos;
			});
		}

		/// <summary>
		/// Reads as bytes.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">filename</exception>
		public Task<byte[]> ReadFromAssetsAsBytesAsync(string filename)
		{
			if (filename.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(filename));

			return Task.Run(() =>
			{
				byte[] file = File.ReadAllBytes(filename);

				return file;
			});
		}

		/// <summary>
		/// Gets the files from internal storage.
		/// </summary>
		/// <param name="directoryName">Name of the directory.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">directoryName</exception>
		public FileInfo[] GetFilesFromInternalStorage(string directoryName)
		{
			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			return GetFilesFromAssetsAsync(directoryName).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		/// <summary>
		/// Saves the file to internal storage;
		/// no other user or apps can access these files.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="folderName">Name of the folder.</param>
		/// <returns>
		/// A string value with the filepath
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// fileName
		/// or
		/// folderName
		/// </exception>
		public string SaveFileToInternalStorage(byte[] file, string fileName, string folderName = "scans")
		{
			if (fileName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fileName));

			if (folderName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(folderName));

			string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), folderName);

			if (!File.Exists(folder))
				Directory.CreateDirectory(folder);

			string filePath = Path.Combine(folder, fileName);
			File.WriteAllBytes(filePath, file);

			return filePath;
		}
	}
}