namespace DCS.User
{
    /// <summary>
    /// Defines the contract for domain-level operations related to user management.
    /// </summary>
    /// <remarks>This interface extends <see cref="IUserDomainRepository"/> to include additional
    /// domain-specific behaviors and rules for managing users. Implementations of this interface should encapsulate
    /// business logic that operates on user entities, ensuring consistency and enforcing domain constraints.</remarks>
    public interface IUserDomainService : IUserDomainRepository
    {
    }
}
