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
	/// <seealso cref="GuidFramework.Services.IFileService" />
	public class FileService : IFileService
	{
		/// <summary>
		/// Clears the internal storage folder.
		/// </summary>
		/// <param name="directoryName">Name of the directory.</param>
		/// <exception cref="ArgumentNullException">directoryName</exception>
		public void ClearInternalStorageFolder(string directoryName = "persons")
		{
			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), directoryName);

			if (Directory.Exists(folder))
				Directory.Delete(folder, true);
		}

		/// <summary>
		/// Deletes the file from internal storage.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <exception cref="ArgumentNullException">fileName</exception>
		public void DeleteFileFromInternalStorage(string fileName)
		{
			if (fileName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fileName));

			if (File.Exists(fileName))
				File.Delete(fileName);
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
		public async Task<byte[]> ReadFromAssetsAsBytesAsync(string filename)
		{
			if (filename.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(filename));

			return await File.ReadAllBytesAsync(filename);
		}

		/// <summary>
		/// Reads from internal storage asynchronous.
		/// </summary>
		/// <param name="fileName">The filename.</param>
		/// <param name="directoryName">The directory name</param>
		/// <returns>
		/// a string value
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// fileName
		/// or
		/// directoryName
		/// </exception>
		public async Task<string> ReadFromInternalStorageAsync(string fileName, string directoryName = "persons")
		{
			if (fileName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fileName));

			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), directoryName);
			string filePath = Path.Combine(folder, fileName);

			return await File.ReadAllTextAsync(filePath);
		}

		/// <summary>
		/// Opens the file from internal storage asynchronous.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="directoryName">Name of the directory.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">
		/// fileName
		/// or
		/// directoryName
		/// </exception>
		public async Task<byte[]> OpenFromInternalStorageAsync(string fileName, string directoryName = "persons")
		{
			if (fileName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fileName));

			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), directoryName);
			string filePath = Path.Combine(folder, fileName);

			return await File.ReadAllBytesAsync(filePath);
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
		public string SaveFileToInternalStorage(byte[] file, string fileName, string folderName = "persons")
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

		/// <summary>
		/// Saves the file to internal storage asynchronous;
		/// no other user or apps can access these files.
		/// </summary>
		/// <param name="fileContents">The file contents.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="directoryName">Name of the folder.</param>
		/// <returns>
		/// A string value with the filepath
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// fileName
		/// or
		/// folderName
		/// </exception>
		public async Task<string> SaveFileToInternalStorageAsync(string fileContents, string fileName, string directoryName = "persons")
		{
			if (fileContents.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fileContents));

			if (fileName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fileName));

			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), directoryName);

			if (!File.Exists(folder))
				Directory.CreateDirectory(folder);

			string filePath = Path.Combine(folder, fileName);
			await File.WriteAllTextAsync(filePath, fileContents);

			return filePath;
		}
	}
}