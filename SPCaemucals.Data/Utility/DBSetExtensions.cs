using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SPCaemucals.Data.Utility;

public static class DbSetExtensions
{
    /// <summary>
    /// Assigns the next ID value to the entity based on the provided ID property string.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <param name="dbSet">The DbSet instance.</param>
    /// <param name="entity">The entity to assign the next ID to.</param>
    /// <param name="idPropertyString">The name of the ID property.</param>
    /// <returns>The entity with the next ID assigned.</returns>
    public static T AssignNextId<T>(this DbSet<T> dbSet, T entity, string idPropertyString) where T : class
    {
        var entityType = typeof(T);
        var idProperty = entityType.GetProperty(idPropertyString);

        var param = Expression.Parameter(entityType, "p");
        var body = Expression.Property(param, idProperty);
        var sortExpression = Expression.Lambda<Func<T, int>>(body, param);

        var lastEntity = dbSet.OrderByDescending(sortExpression).FirstOrDefault();

        if (lastEntity != null)
        {
            var lastId = (int)idProperty.GetValue(lastEntity);
            idProperty.SetValue(entity, lastId + 1);
        }
        else
        {
            idProperty.SetValue(entity, 1);
        }

        return entity;
    }
}