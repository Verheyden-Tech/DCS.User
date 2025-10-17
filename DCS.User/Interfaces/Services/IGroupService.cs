﻿using DCS.CoreLib;

namespace DCS.User
{
    /// <summary>
    /// Represents the GroupService for <see cref="Group"/> entities.
    /// </summary>
    public interface IGroupService : IServiceBase<Guid, Group, IGroupRepository>
    {
    }
}
