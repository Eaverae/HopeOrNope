using Android.Content.Res;
using GuidFramework.Droid.Services;
using GuidFramework.Extensions;
using GuidFramework.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace GuidFramework.Droid.Services
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

			string folder = Path.Combine(GuidFrameworkActivity.CurrentActivity.ApplicationContext.FilesDir.Path, directoryName);

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
		/// Gets the files from the assets folder
		/// </summary>
		/// <param name="directoryName">Name of the directory.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">directoryName</exception>
		public async Task<FileInfo[]> GetFilesFromAssetsAsync(string directoryName)
		{
			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			FileInfo[] fileInfos = null;

			using (AssetManager assets = GuidFrameworkActivity.CurrentActivity.ApplicationContext.Assets)
			{
				string[] files = await assets.ListAsync(directoryName);

				if (files != null && files.Any())
					fileInfos = files.Select(item => new FileInfo(item)).OrderBy(item => item.Name).ToArray();
			}

			return fileInfos;
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

			FileInfo[] fileInfos = null;

			string[] files = Directory.GetFiles(directoryName);

			if (files != null && files.Any())
				fileInfos = files.Select(item => new FileInfo(item)).OrderBy(item => item.Name).ToArray();

			return fileInfos;
		}

		/// <summary>
		/// Opens the file from internal storage asynchronous.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="directoryName">Name of the directory.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">fileName</exception>
		public async Task<byte[]> OpenFromInternalStorageAsync(string fileName, string directoryName = "persons")
		{
			if (fileName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fileName));

			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			byte[] result = null;

			string folder = Path.Combine(GuidFrameworkActivity.CurrentActivity.ApplicationContext.FilesDir.Path, directoryName);
			Java.IO.File newFile = new Java.IO.File(folder, fileName);

			if (newFile.Exists())
				result = await File.ReadAllBytesAsync(newFile.Path);

			return result;
		}

		/// <summary>
		/// Reads the file as bytes.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">filename</exception>
		public async Task<byte[]> ReadFromAssetsAsBytesAsync(string filename)
		{
			if (filename.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(filename));

			byte[] file = null;

			using (AssetManager assets = GuidFrameworkActivity.CurrentActivity.ApplicationContext.Assets)
			using (MemoryStream memoryStream = new MemoryStream())
			using (Stream stream = assets.Open(filename))
			{
				await stream.CopyToAsync(memoryStream);
				file = memoryStream.ToArray();
			}

			return file;
		}

		/// <summary>
		/// Reads from internal storage asynchronous.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="directoryName">Name of the folder.</param>
		/// <returns>
		/// a string value
		/// </returns>
		/// <exception cref="ArgumentNullException">filename</exception>
		public async Task<string> ReadFromInternalStorageAsync(string filename, string directoryName = "persons")
		{
			if (filename.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(filename));

			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			string fileContents = string.Empty;
			string folder = Path.Combine(GuidFrameworkActivity.CurrentActivity.ApplicationContext.FilesDir.Path, directoryName);
			Java.IO.File newFile = new Java.IO.File(folder, filename);

			if (newFile.Exists())
				fileContents = await File.ReadAllTextAsync(newFile.Path);

			return fileContents;
		}

		/// <summary>
		/// Saves the file to internal storage;
		/// no other user or apps can access these files.
		/// </summary>
		/// <param name="file">The file.</param>
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
		public string SaveFileToInternalStorage(byte[] file, string fileName, string directoryName = "persons")
		{
			if (fileName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fileName));

			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			// Save files to internal storage; no other user or apps can access these files.
			// Unlike the external storage directories, your app does not require any system permissions to read and write to the internal directories returned by these methods.
			string folder = Path.Combine(GuidFrameworkActivity.CurrentActivity.ApplicationContext.FilesDir.Path, directoryName);
			Java.IO.File newFile = new Java.IO.File(folder, fileName);

			if (!newFile.Exists())
				newFile.ParentFile?.Mkdirs();

			File.WriteAllBytes(newFile.Path, file);

			return newFile.Path;
		}

		/// <summary>
		/// Saves the file to internal storage asynchronous.
		/// </summary>
		/// <param name="fileContents">The file contents.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="directoryName">Name of the directory.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">
		/// fileName
		/// or
		/// directoryName
		/// </exception>
		public async Task<string> SaveFileToInternalStorageAsync(string fileContents, string fileName, string directoryName = "persons")
		{
			if (fileContents.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fileContents));

			if (fileName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fileName));

			if (directoryName.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(directoryName));

			// Save files to internal storage; no other user or apps can access these files.
			// Unlike the external storage directories, your app does not require any system permissions to read and write to the internal directories returned by these methods.
			string folder = Path.Combine(GuidFrameworkActivity.CurrentActivity.ApplicationContext.FilesDir.Path, directoryName);
			Java.IO.File newFile = new Java.IO.File(folder, fileName);

			if (!newFile.Exists())
				newFile.ParentFile?.Mkdirs();

			await File.WriteAllTextAsync(newFile.Path, fileContents);

			return newFile.Path;
		}
	}
}