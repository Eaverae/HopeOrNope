using GuidFramework.Classes;
using GuidFramework.Extensions;
using GuidFramework.Interfaces;
using GuidFramework.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuidFramework.Handlers
{
	/// <summary>
	/// LocalStorageHandler which handles the storage and retrieval of entities in the filesystem
	/// </summary>
	/// <seealso cref="GuidFramework.Interfaces.ILocalStorageHandler" />
	public class LocalStorageHandler : ILocalStorageHandler
	{
		private IFileService fileService;

		/// <summary>
		/// Lists the entities asynchronous.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <returns></returns>
		public async Task<IEnumerable<TEntity>> ListAsync<TEntity>()
			where TEntity : BaseEntity
		{
			if (fileService == null)
				fileService = DependencyService.Get<IFileService>();

			IEnumerable<TEntity> entities = null;

			string fileName = typeof(TEntity).FullName.RemoveSpecialCharacters();

			if (!fileName.IsNullOrWhiteSpace())
			{
				string fileContent = await fileService.ReadFromInternalStorageAsync(fileName);

				if (!fileContent.IsNullOrWhiteSpace())
					entities = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(fileContent);
			}

			return entities;
		}

		/// <summary>
		/// Return the specified entity for the given identifier.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public async Task<TEntity> Details<TEntity>(string id)
			where TEntity : BaseEntity
		{
			return (await ListAsync<TEntity>()).SingleOrDefault(item => item.Id.Equals(id));
		}

		/// <summary>
		/// Saves the given entity asynchronous.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		public async Task<bool> SaveAsync<TEntity>(TEntity entity)
			where TEntity : BaseEntity
		{
			if (fileService == null)
				fileService = DependencyService.Get<IFileService>();

			string fileName = typeof(TEntity).FullName.RemoveSpecialCharacters();
			string fileContents = string.Empty;
			string result = string.Empty;

			List<TEntity> entities = (await ListAsync<TEntity>()).ToList();

			// Remove when exists
			if (!entity.Id.IsNullOrWhiteSpace())
			{
				TEntity existing = entities.Where(item => item.Id.Equals(entity.Id)).SingleOrDefault();
				if (existing != null)
					entities.Remove(existing);
			}

			// Add the entity
			entities.Add(entity);

			// Serialize the entities before writing the files
			fileContents = JsonConvert.SerializeObject(entities);

			if (!fileName.IsNullOrWhiteSpace())
				result = await fileService.SaveFileToInternalStorageAsync(fileContents, fileName);

			return !result.IsNullOrWhiteSpace();
		}

		/// <summary>
		/// Deletes the given entity asynchronous.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		public async Task<bool> DeleteAsync<TEntity>(TEntity entity)
			where TEntity : BaseEntity
		{
			if (fileService == null)
				fileService = DependencyService.Get<IFileService>();

			string fileName = typeof(TEntity).FullName.RemoveSpecialCharacters();

			bool result = false;
			List<TEntity> entities = (await ListAsync<TEntity>()).ToList();

			// Remove when exists
			if (!entity.Id.IsNullOrWhiteSpace())
			{
				TEntity existing = entities.Where(item => item.Id.Equals(entity.Id)).SingleOrDefault();

				if (existing != null)
					result = entities.Remove(existing);
			}

			// Serialize the entities before writing the files
			string fileContents = JsonConvert.SerializeObject(entities);

			if (!fileName.IsNullOrWhiteSpace())
				result = !(await fileService.SaveFileToInternalStorageAsync(fileContents, fileName)).IsNullOrWhiteSpace();

			return result;
		}
	}
}
