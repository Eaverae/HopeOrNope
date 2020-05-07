using GuidFramework.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuidFramework.Interfaces
{
	/// <summary>
	/// LocalStorageHandler interface for saving objects to local storage
	/// </summary>
	public interface ILocalStorageHandler
	{
		/// <summary>
		/// Lists the entities asynchronous.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <returns></returns>
		Task<IEnumerable<TEntity>> ListAsync<TEntity>()
			where TEntity : BaseEntity;

		/// <summary>
		/// Return the specified entity for the given identifier.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task<TEntity> Details<TEntity>(string id)
			where TEntity : BaseEntity;

		/// <summary>
		/// Saves the given entity asynchronous.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		Task<bool> SaveAsync<TEntity>(TEntity entity)
			where TEntity : BaseEntity;

		/// <summary>
		/// Deletes the given entity asynchronous.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		Task<bool> DeleteAsync<TEntity>(TEntity entity)
			where TEntity : BaseEntity;
	}
}
