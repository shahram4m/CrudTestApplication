namespace CrudTestApplication.Core.Entities.Base
{
    public interface IEntityBase<TId>
    {
        TId Id { get; }
    }
}
